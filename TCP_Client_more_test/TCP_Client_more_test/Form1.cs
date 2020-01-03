using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using System.Net.Sockets;

namespace TCP_Client_more_test
{
    public partial class Form1 : Form
    {
        //private List<AsyncTcpClient> asyncClientlist;
        private List<AsyncPerformer> asyncPerformerlist;
        public int testnum = 0;

        public Form1()
        {
            InitializeComponent();
            readORwrite.SelectedIndex = 0;
            timer1.Interval = 1000;
        }

        private void Connectbutton_Click(object sender, EventArgs e)
        {
            //asyncClientlist = new List<AsyncTcpClient>();
            //for (int i = 0; i < int.Parse(ConnectNum.Text); i++)
            //{
            //    asyncClientlist.Add(new AsyncTcpClient(DestinationIP.Text, int.Parse(DestinationPort.Text)));
            //}
            //foreach (AsyncTcpClient atc in asyncClientlist)
            //{
            //    atc.Connect();
            //}

            asyncPerformerlist = new List<AsyncPerformer>();
            for (int i = 0; i < int.Parse(ConnectNum.Text); i++)
            {
                AsyncPerformer apf = new AsyncPerformer();
                apf.Timeout = 8000;
                apf.Interval = 1000;
                apf.OnStart += TCPOnStart;
                apf.OnStop += TCPOnStop;
                apf.OnAsyncWork += TCPOnAsyncWork;
                if (readORwrite.SelectedIndex == 1)
                {
                    apf.readFlag = true;
                }
                if (readORwrite.SelectedIndex == 2)
                {
                    apf.writeFlag = true;
                }
                apf.Start();
                asyncPerformerlist.Add(apf);
            }
            debug_text.Clear();
            testnum = 0;
            timer1.Start();
            Connectbutton.Enabled = false;
            Disconnectbutton.Enabled = true;
        }

        private void Disconnectbutton_Click(object sender, EventArgs e)
        {
            //if (asyncClientlist.Count == 0)
            //    return;
            //foreach (AsyncTcpClient atc in asyncClientlist)
            //{
            //    try { using (atc) { } }
            //    finally {}
            //}
            //asyncClientlist.Clear();

            foreach (AsyncPerformer apf in asyncPerformerlist)
            {
                apf.Stop();
            }
            debug_text.Clear();
            timer1.Stop();
            Disconnectbutton.Enabled = false;
            Connectbutton.Enabled = true;
        }

        private void TCPOnAsyncWork(AsyncPerformer apf, ref bool reqNewThread, ref object userState)
        {
            byte[] readbyte = { 0x01, 0x01, 0x00, 0x00, 0x00, 0x04, 0x3D, 0xC9 };
            byte[] readbyte2 = { 0x01, 0x01, 0x00, 0x00, 0x00, 0x04, 0x3D, 0xC9 };
            byte[] writebyte = { 0x01, 0x05, 0x00, 0x10, 0x00, 0x00, 0xCC, 0x0F };
            if (apf.readFlag)
            {
                apf.userTcp.Client.Send(readbyte);
                byte[] receive = new byte[10];
                apf.userTcp.Client.Receive(receive);
                if (receive[1] == 0x01)
                {
                    apf.readsuccess = true;
                }
                else
                {
                    apf.readsuccess = false;
                }
            }
            if (apf.writeFlag)
            {
                apf.userTcp.Client.Send(writebyte);
                byte[] receive = new byte[10];
                apf.userTcp.Client.Receive(receive);
                if (receive[1] == 0x05)
                {
                    apf.writesuccess = true;
                }
                else
                {
                    apf.writesuccess = false;
                }
            }
        }

        private void TCPOnStart(AsyncPerformer performer, ref object userState)
        {
            if (performer.userTcp == null)
            {
                performer.userTcp = new TcpClient();
                performer.userTcp.Connect(DestinationIP.Text, int.Parse(DestinationPort.Text));
            }
            else if (!performer.userTcp.Connected)
            {
                performer.userTcp.Connect(DestinationIP.Text, int.Parse(DestinationPort.Text));
            }
            performer.userTcp.Client.SendTimeout = 500;
            performer.userTcp.Client.ReceiveTimeout = 500;
        }

        private void TCPOnStop(AsyncPerformer performer, ref object userState)
        {
            if (performer.userTcp != null)
            {
                try
                {
                    performer.userTcp.Close();
                }
                finally { }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            debug_text.Clear();
            testnum++;
            int connectnum = 0;
            int readnum = 0;
            int writenum = 0;

            foreach (AsyncPerformer apf in asyncPerformerlist)
            {
                if (apf.userTcp.Connected)
                {
                    connectnum++;
                    if (apf.readFlag)
                    {
                        if (apf.readsuccess)
                        {
                            readnum++;
                        }
                    }
                    if (apf.writeFlag)
                    {
                        if (apf.writesuccess)
                        {
                            writenum++;
                        }
                    }
                }
            }
            debug_text.AppendText(string.Format("已连接：{0} 未连接：{1}\r\n", connectnum, asyncPerformerlist.Count - connectnum));
            debug_text.AppendText(string.Format("已读取：{0} 未读取：{1}\r\n", readnum, asyncPerformerlist.Count - readnum));
            debug_text.AppendText(string.Format("已写入：{0} 未写入：{1}\r\n", writenum, asyncPerformerlist.Count - writenum));
            debug_text.AppendText(string.Format("当前时间：{0}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            debug_text.AppendText(string.Format("次数：{0}", testnum));
        }
    }

    public class AsyncPerformer : IDisposable
    {
        private ThreadStart asyncStart = null;
        private Thread thread = null;
        private System.Timers.Timer delayer = new System.Timers.Timer();
        private bool stopSignal = true;
        private bool asyncWorking = false;
        private bool timeoutCtrlFlag = false;
        private bool reqNewThread = false;
        public uint Timeout = 0;//=0: infinity; >0: milliseconds
        public object userState = null;

        public TcpClient userTcp = null;
        public bool readFlag = false;
        public bool writeFlag = false;
        public bool readsuccess = false;
        public bool writesuccess = false;

        [System.Runtime.InteropServices.DllImport("winmm.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern UInt32 timeGetTime();
        /// <summary>
        /// 返回操作系统启动到现在所经过的毫秒数
        /// </summary>
        /// <returns></returns>
        public static UInt32 GetTickCount()
        {
            return timeGetTime();
        }

        public AsyncPerformer()
        {
            asyncStart = new ThreadStart(DoWork);
            delayer.Interval = 8;
            delayer.Elapsed += delayerElapsed;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public delegate void WorkHandler(AsyncPerformer performer, ref bool reqNewThread, ref object userState);
        public event WorkHandler OnAsyncWork = null;
        protected virtual void AsyncWork(ref bool reqNewThread, ref object userState) { }
        private int revisedInterval = 0;
        private int interval = 100;
        public int Interval
        {
            get { return interval; }
            set
            {
                if (value < 2)
                    value = 2;
                if (value > 18000000)
                    value = 18000000;
                interval = value;
                revisedInterval = interval;
            }
        }
        public uint LoopTimes = 0;//=0: endless; >0: loop specified times.
        private uint loopCount = 0;

        private void DoWork()
        {
            try
            {
                reqNewThread = false;

                while (!stopSignal)
                {
                    ////sp.WaitOne();//Wait for next work
                    UInt32 tickCnt1 = GetTickCount();

                    if (Timeout > 0)
                    {
                        timeoutCtrlFlag = true;
                        delayer.Interval = (int)Timeout;
                        delayer.Start();//Start timeout counting
                    }

                    try
                    {
                        if (OnAsyncWork != null)
                        {
                            OnAsyncWork(this, ref reqNewThread, ref userState);
                        }
                        else
                        {
                            AsyncWork(ref reqNewThread, ref userState);
                        }
                    }
                    catch// (Exception ex)
                    {
                        //string errStr = ex.Message + " ";
                        //if (ex.GetBaseException() != null)
                        //{
                        //    errStr += ex.GetBaseException().Message;
                        //}
                        //MsgOut(this, "Error02", this.GetType().Name + ": " + errStr, 65535);
                        //MsgOut(this, "TCP communication error", ex.Message, 65535);
                    }

                    if (Timeout > 0)
                    {
                        timeoutCtrlFlag = false;
                        delayer.Stop();//Stop timeout counting
                    }

                    UInt32 tickCnt2 = GetTickCount();
                    if (tickCnt1 > tickCnt2)
                    {//若操作系统连续运行2^32-1 = 4,294,967,295毫秒，这个数字会回到0
                        revisedInterval = interval - (int)(4294967295 - tickCnt1 + 1 + tickCnt2);
                    }
                    else
                    {
                        revisedInterval = interval - (int)(tickCnt2 - tickCnt1);
                    }
                    //MsgOut(this, "Info", "Revised interval is " + revisedInterval.ToString() + ".", 2);

                    if (LoopTimes > 0)
                    {//默认无限循环
                        loopCount++;
                        if (loopCount >= LoopTimes)
                        {
                            stopSignal = true;
                        }
                    }

                    if (stopSignal)
                    {
                        asyncWorking = false;
                        return;
                    }

                    if (revisedInterval > 0)
                    {
                        Thread.Sleep(revisedInterval);
                    }
                    else
                    {
                        Thread.Sleep(1);
                    }
                }
            }
            catch (ThreadAbortException)
            {
                if (stopSignal)
                {
                    //DoAfterStop(2);
                    return;
                }
                reqNewThread = true;
            }
            catch
            {
                reqNewThread = true;
                return;
            }

            lock (asyncStart)
            {
                if (asyncWorking)
                {
                    asyncWorking = false;
                    //DoAfterStop(3);
                }
            }
        }

        void delayerElapsed(object sender, ElapsedEventArgs e)
        {
            delayer.Stop();

            if (stopSignal)
            {
                //DoAfterStop(1);
                return;
            }

            lock (this)
            {
                asyncWorking = true;
            }

            try
            {
                if (timeoutCtrlFlag)//If do work timeout
                {//超时
                    timeoutCtrlFlag = false;
                    if (thread != null)
                    {
                        try { thread.Abort(); }
                        catch { }
                        thread = null;
                    }
                }

                if (reqNewThread || (thread == null))//If the worker thread has to be renew
                {//线程更新
                    if (thread != null)
                    {
                        try { thread.Abort(); }
                        catch { }
                        thread = null;
                    }
                    //MsgOut(this, "Note", this.GetType().Name + ": About to create new thread.", 2);
                    try
                    {
                        thread = new Thread(asyncStart);
                        thread.Start();
                    }
                    catch
                    {
                        delayer.Interval = 66;
                        delayer.Start();//Retry to create thread
                        return;
                    }
                }

                //if (Timeout > 0)
                //{
                //    timeoutCtrlFlag = true;
                //    delayer.Interval = (int)Timeout;
                //    delayer.Start();
                //}
                ////sp.Release();//Release next work
            }
            catch// (Exception ex)
            {
                //MsgOut(this, "Error01", this.GetType().Name + ": " + ex.Message, 65535);
            }
        }

        public delegate void StateHandler(AsyncPerformer performer, ref object userState);
        public event StateHandler OnStart = null;
        public event StateHandler OnStop = null;

        public void Start()
        {
            lock (this)
            {
                if (asyncWorking)
                {
                    return;
                }
                loopCount = 0;
                stopSignal = false;
                timeoutCtrlFlag = false;
                reqNewThread = true;
                delayer.Interval = interval;
                delayer.Start();
            }

            if (OnStart != null)
            {
                OnStart(this, ref userState);
            }

            //AfterStart(userState);
        }

        public void Stop()
        {
            if (OnStop != null)
            {
                OnStop(this, ref userState);
            }

            delayer.Stop();
            stopSignal = true;
            if (Stopped)
            {
                //DoAfterStop(4);
                return;
            }

            lock (asyncStart)
            {
                if (asyncWorking)
                {
                    asyncWorking = false;
                    if (thread != null)
                    {
                        try { thread.Abort(); }
                        catch { }
                        thread = null;
                    }
                    //DoAfterStop(5);
                }
            }
        }

        public bool Stopped
        {
            get
            {
                if (stopSignal && (!asyncWorking)) return true;
                return false;
            }
        }
    }
}
