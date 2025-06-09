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
            logBox = new System.Windows.Forms.ListBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            timer = new System.Windows.Forms.Timer(components);
            groupBox2 = new System.Windows.Forms.GroupBox();
            rootFldTb = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            openRootBtn = new System.Windows.Forms.Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // logBox
            // 
            logBox.Dock = System.Windows.Forms.DockStyle.Fill;
            logBox.FormattingEnabled = true;
            logBox.ItemHeight = 15;
            logBox.Location = new System.Drawing.Point(3, 19);
            logBox.Name = "logBox";
            logBox.Size = new System.Drawing.Size(497, 260);
            logBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(logBox);
            groupBox1.Location = new System.Drawing.Point(12, 156);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(503, 282);
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
            groupBox2.Controls.Add(openRootBtn);
            groupBox2.Controls.Add(rootFldTb);
            groupBox2.Controls.Add(label1);
            groupBox2.Location = new System.Drawing.Point(15, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(497, 138);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Status";
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
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(527, 450);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "MainForm";
            Text = "Sim Monitor";
            Load += MainForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
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
    }
}