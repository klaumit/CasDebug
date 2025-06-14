namespace SimMon
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            logBox = new System.Windows.Forms.ListBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            timer = new System.Windows.Forms.Timer(components);
            groupBox2 = new System.Windows.Forms.GroupBox();
            cmdBtn = new System.Windows.Forms.Button();
            openRootBtn = new System.Windows.Forms.Button();
            rootFldTb = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            imgLst = new System.Windows.Forms.ImageList(components);
            groupBox3 = new System.Windows.Forms.GroupBox();
            maxiDumpBtn = new System.Windows.Forms.Button();
            screenBtn = new System.Windows.Forms.Button();
            miniDumpBtn = new System.Windows.Forms.Button();
            simLstV = new System.Windows.Forms.ListView();
            handleBtn = new System.Windows.Forms.Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // logBox
            // 
            logBox.Dock = System.Windows.Forms.DockStyle.Fill;
            logBox.FormattingEnabled = true;
            logBox.ItemHeight = 15;
            logBox.Location = new System.Drawing.Point(3, 19);
            logBox.Name = "logBox";
            logBox.Size = new System.Drawing.Size(497, 165);
            logBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(logBox);
            groupBox1.Location = new System.Drawing.Point(12, 270);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(503, 187);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Log";
            // 
            // timer
            // 
            timer.Interval = 1000;
            timer.Tick += timer_Tick;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(cmdBtn);
            groupBox2.Controls.Add(openRootBtn);
            groupBox2.Controls.Add(rootFldTb);
            groupBox2.Controls.Add(label1);
            groupBox2.Location = new System.Drawing.Point(15, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(497, 69);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Status";
            // 
            // cmdBtn
            // 
            cmdBtn.Location = new System.Drawing.Point(421, 25);
            cmdBtn.Name = "cmdBtn";
            cmdBtn.Size = new System.Drawing.Size(56, 23);
            cmdBtn.TabIndex = 3;
            cmdBtn.Text = "Cmd";
            cmdBtn.UseVisualStyleBackColor = true;
            cmdBtn.Click += cmdBtn_Click;
            // 
            // openRootBtn
            // 
            openRootBtn.Location = new System.Drawing.Point(340, 25);
            openRootBtn.Name = "openRootBtn";
            openRootBtn.Size = new System.Drawing.Size(75, 23);
            openRootBtn.TabIndex = 2;
            openRootBtn.Text = "Open";
            openRootBtn.UseVisualStyleBackColor = true;
            openRootBtn.Click += openRootBtn_Click;
            // 
            // rootFldTb
            // 
            rootFldTb.Location = new System.Drawing.Point(96, 25);
            rootFldTb.Name = "rootFldTb";
            rootFldTb.Size = new System.Drawing.Size(238, 23);
            rootFldTb.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(21, 28);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(69, 15);
            label1.TabIndex = 0;
            label1.Text = "Root folder:";
            // 
            // imgLst
            // 
            imgLst.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            imgLst.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imgLst.ImageStream");
            imgLst.TransparentColor = System.Drawing.Color.Transparent;
            imgLst.Images.SetKeyName(0, "Sim.png");
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(handleBtn);
            groupBox3.Controls.Add(maxiDumpBtn);
            groupBox3.Controls.Add(screenBtn);
            groupBox3.Controls.Add(miniDumpBtn);
            groupBox3.Controls.Add(simLstV);
            groupBox3.Location = new System.Drawing.Point(15, 87);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(497, 177);
            groupBox3.TabIndex = 4;
            groupBox3.TabStop = false;
            groupBox3.Text = "Found";
            // 
            // maxiDumpBtn
            // 
            maxiDumpBtn.Location = new System.Drawing.Point(351, 82);
            maxiDumpBtn.Name = "maxiDumpBtn";
            maxiDumpBtn.Size = new System.Drawing.Size(75, 22);
            maxiDumpBtn.TabIndex = 6;
            maxiDumpBtn.Text = "MaxiDump";
            maxiDumpBtn.UseVisualStyleBackColor = true;
            maxiDumpBtn.Click += maxiDmpBtn_Click;
            // 
            // screenBtn
            // 
            screenBtn.Location = new System.Drawing.Point(351, 50);
            screenBtn.Name = "screenBtn";
            screenBtn.Size = new System.Drawing.Size(75, 23);
            screenBtn.TabIndex = 5;
            screenBtn.Text = "Screenshot";
            screenBtn.UseVisualStyleBackColor = true;
            screenBtn.Click += screenBtn_Click;
            // 
            // miniDumpBtn
            // 
            miniDumpBtn.Location = new System.Drawing.Point(349, 22);
            miniDumpBtn.Name = "miniDumpBtn";
            miniDumpBtn.Size = new System.Drawing.Size(77, 22);
            miniDumpBtn.TabIndex = 4;
            miniDumpBtn.Text = "MiniDump";
            miniDumpBtn.UseVisualStyleBackColor = true;
            miniDumpBtn.Click += miniDumpBtn_Click;
            // 
            // simLstV
            // 
            simLstV.LargeImageList = imgLst;
            simLstV.Location = new System.Drawing.Point(21, 22);
            simLstV.Name = "simLstV";
            simLstV.Size = new System.Drawing.Size(322, 139);
            simLstV.TabIndex = 3;
            simLstV.UseCompatibleStateImageBehavior = false;
            simLstV.MouseDoubleClick += simLstV_MouseDoubleClick;
            // 
            // handleBtn
            // 
            handleBtn.Location = new System.Drawing.Point(351, 115);
            handleBtn.Name = "handleBtn";
            handleBtn.Size = new System.Drawing.Size(75, 22);
            handleBtn.TabIndex = 7;
            handleBtn.Text = "Handles";
            handleBtn.UseVisualStyleBackColor = true;
            handleBtn.Click += handleBtn_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(527, 469);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "MainForm";
            Text = "Sim Monitor";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListBox logBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox rootFldTb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button openRootBtn;
        private System.Windows.Forms.ImageList imgLst;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView simLstV;
        private System.Windows.Forms.Button cmdBtn;
        private System.Windows.Forms.Button miniDumpBtn;
        private System.Windows.Forms.Button screenBtn;
        private System.Windows.Forms.Button maxiDumpBtn;
        private System.Windows.Forms.Button handleBtn;
    }
}