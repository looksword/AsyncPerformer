namespace TCP_Client_more_test
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DestinationIP = new System.Windows.Forms.TextBox();
            this.DestinationPort = new System.Windows.Forms.TextBox();
            this.Connectbutton = new System.Windows.Forms.Button();
            this.Disconnectbutton = new System.Windows.Forms.Button();
            this.ConnectNum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.debug_text = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.readORwrite = new System.Windows.Forms.ComboBox();
            this.listenPort = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ip";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(156, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "port";
            // 
            // DestinationIP
            // 
            this.DestinationIP.Location = new System.Drawing.Point(55, 20);
            this.DestinationIP.Name = "DestinationIP";
            this.DestinationIP.Size = new System.Drawing.Size(95, 21);
            this.DestinationIP.TabIndex = 2;
            this.DestinationIP.Text = "192.168.1.200";
            // 
            // DestinationPort
            // 
            this.DestinationPort.Location = new System.Drawing.Point(191, 20);
            this.DestinationPort.Name = "DestinationPort";
            this.DestinationPort.Size = new System.Drawing.Size(47, 21);
            this.DestinationPort.TabIndex = 3;
            this.DestinationPort.Text = "4196";
            // 
            // Connectbutton
            // 
            this.Connectbutton.Location = new System.Drawing.Point(500, 15);
            this.Connectbutton.Name = "Connectbutton";
            this.Connectbutton.Size = new System.Drawing.Size(67, 29);
            this.Connectbutton.TabIndex = 4;
            this.Connectbutton.Text = "Connect";
            this.Connectbutton.UseVisualStyleBackColor = true;
            this.Connectbutton.Click += new System.EventHandler(this.Connectbutton_Click);
            // 
            // Disconnectbutton
            // 
            this.Disconnectbutton.Location = new System.Drawing.Point(613, 15);
            this.Disconnectbutton.Name = "Disconnectbutton";
            this.Disconnectbutton.Size = new System.Drawing.Size(85, 29);
            this.Disconnectbutton.TabIndex = 5;
            this.Disconnectbutton.Text = "Disconnect";
            this.Disconnectbutton.UseVisualStyleBackColor = true;
            this.Disconnectbutton.Click += new System.EventHandler(this.Disconnectbutton_Click);
            // 
            // ConnectNum
            // 
            this.ConnectNum.Location = new System.Drawing.Point(285, 20);
            this.ConnectNum.Name = "ConnectNum";
            this.ConnectNum.Size = new System.Drawing.Size(47, 21);
            this.ConnectNum.TabIndex = 7;
            this.ConnectNum.Text = "100";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(250, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Num";
            // 
            // debug_text
            // 
            this.debug_text.Location = new System.Drawing.Point(72, 113);
            this.debug_text.Multiline = true;
            this.debug_text.Name = "debug_text";
            this.debug_text.Size = new System.Drawing.Size(495, 161);
            this.debug_text.TabIndex = 8;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // readORwrite
            // 
            this.readORwrite.FormattingEnabled = true;
            this.readORwrite.Items.AddRange(new object[] {
            "none",
            "read",
            "write"});
            this.readORwrite.Location = new System.Drawing.Point(346, 22);
            this.readORwrite.Name = "readORwrite";
            this.readORwrite.Size = new System.Drawing.Size(129, 20);
            this.readORwrite.TabIndex = 9;
            // 
            // listenPort
            // 
            this.listenPort.Location = new System.Drawing.Point(286, 65);
            this.listenPort.Name = "listenPort";
            this.listenPort.Size = new System.Drawing.Size(47, 21);
            this.listenPort.TabIndex = 11;
            this.listenPort.Text = "30000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(191, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "起始监听端口号";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 394);
            this.Controls.Add(this.listenPort);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.readORwrite);
            this.Controls.Add(this.debug_text);
            this.Controls.Add(this.ConnectNum);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Disconnectbutton);
            this.Controls.Add(this.Connectbutton);
            this.Controls.Add(this.DestinationPort);
            this.Controls.Add(this.DestinationIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "TCP多重连接测试";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DestinationIP;
        private System.Windows.Forms.TextBox DestinationPort;
        private System.Windows.Forms.Button Connectbutton;
        private System.Windows.Forms.Button Disconnectbutton;
        private System.Windows.Forms.TextBox ConnectNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox debug_text;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox readORwrite;
        private System.Windows.Forms.TextBox listenPort;
        private System.Windows.Forms.Label label4;
    }
}

