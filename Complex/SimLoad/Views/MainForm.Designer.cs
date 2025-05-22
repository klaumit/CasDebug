using System.ComponentModel;
using System.Windows.Forms;

namespace SimLoad.Views;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        debugBox = new ListBox();
        closeBtn = new Button();
        tryBtn = new Button();
        disBtn = new Button();
        keyInitBtn = new Button();
        hideKeyBtn = new Button();
        initDispBtn = new Button();
        onOffBtn = new Button();
        refreshBtn = new Button();
        SuspendLayout();
        // 
        // debugBox
        // 
        debugBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        debugBox.FormattingEnabled = true;
        debugBox.ItemHeight = 15;
        debugBox.Location = new System.Drawing.Point(12, 12);
        debugBox.Name = "debugBox";
        debugBox.Size = new System.Drawing.Size(563, 169);
        debugBox.TabIndex = 0;
        // 
        // closeBtn
        // 
        closeBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        closeBtn.Location = new System.Drawing.Point(500, 292);
        closeBtn.Name = "closeBtn";
        closeBtn.Size = new System.Drawing.Size(75, 23);
        closeBtn.TabIndex = 1;
        closeBtn.Text = "Close";
        closeBtn.UseVisualStyleBackColor = true;
        closeBtn.Click += closeBtn_Click;
        // 
        // tryBtn
        // 
        tryBtn.Location = new System.Drawing.Point(12, 292);
        tryBtn.Name = "tryBtn";
        tryBtn.Size = new System.Drawing.Size(75, 23);
        tryBtn.TabIndex = 2;
        tryBtn.Text = "Try";
        tryBtn.UseVisualStyleBackColor = true;
        tryBtn.Click += tryBtn_Click;
        // 
        // disBtn
        // 
        disBtn.Location = new System.Drawing.Point(81, 196);
        disBtn.Name = "disBtn";
        disBtn.Size = new System.Drawing.Size(119, 23);
        disBtn.TabIndex = 3;
        disBtn.Text = "Disasm a lot";
        disBtn.UseVisualStyleBackColor = true;
        disBtn.Click += disBtn_Click;
        // 
        // keyInitBtn
        // 
        keyInitBtn.Location = new System.Drawing.Point(215, 196);
        keyInitBtn.Name = "keyInitBtn";
        keyInitBtn.Size = new System.Drawing.Size(121, 23);
        keyInitBtn.TabIndex = 4;
        keyInitBtn.Text = "Init the keys";
        keyInitBtn.UseVisualStyleBackColor = true;
        keyInitBtn.Click += keyInitBtn_Click;
        // 
        // hideKeyBtn
        // 
        hideKeyBtn.Location = new System.Drawing.Point(353, 196);
        hideKeyBtn.Name = "hideKeyBtn";
        hideKeyBtn.Size = new System.Drawing.Size(110, 23);
        hideKeyBtn.TabIndex = 5;
        hideKeyBtn.Text = "Show/Hide keys";
        hideKeyBtn.UseVisualStyleBackColor = true;
        hideKeyBtn.Click += hideKeyBtn_Click;
        // 
        // initDispBtn
        // 
        initDispBtn.Location = new System.Drawing.Point(215, 225);
        initDispBtn.Name = "initDispBtn";
        initDispBtn.Size = new System.Drawing.Size(121, 23);
        initDispBtn.TabIndex = 6;
        initDispBtn.Text = "Init the display";
        initDispBtn.UseVisualStyleBackColor = true;
        initDispBtn.Click += initDispBtn_Click;
        // 
        // onOffBtn
        // 
        onOffBtn.Location = new System.Drawing.Point(353, 225);
        onOffBtn.Name = "onOffBtn";
        onOffBtn.Size = new System.Drawing.Size(110, 23);
        onOffBtn.TabIndex = 7;
        onOffBtn.Text = "On/Off Display";
        onOffBtn.UseVisualStyleBackColor = true;
        onOffBtn.Click += onOffBtn_Click;
        // 
        // refreshBtn
        // 
        refreshBtn.Location = new System.Drawing.Point(215, 254);
        refreshBtn.Name = "refreshBtn";
        refreshBtn.Size = new System.Drawing.Size(121, 23);
        refreshBtn.TabIndex = 8;
        refreshBtn.Text = "Refresh";
        refreshBtn.UseVisualStyleBackColor = true;
        refreshBtn.Click += refreshBtn_Click;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(587, 327);
        Controls.Add(refreshBtn);
        Controls.Add(onOffBtn);
        Controls.Add(initDispBtn);
        Controls.Add(hideKeyBtn);
        Controls.Add(keyInitBtn);
        Controls.Add(disBtn);
        Controls.Add(tryBtn);
        Controls.Add(closeBtn);
        Controls.Add(debugBox);
        Name = "MainForm";
        Text = "Sim Loader";
        Load += MainForm_Load;
        ResumeLayout(false);
    }

    #endregion

    private ListBox debugBox;
    private Button closeBtn;
    private Button tryBtn;
    private Button disBtn;
    private Button keyInitBtn;
    private Button hideKeyBtn;
    private Button initDispBtn;
    private Button onOffBtn;
    private Button refreshBtn;
}
