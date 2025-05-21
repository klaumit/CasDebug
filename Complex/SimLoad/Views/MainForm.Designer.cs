using System.ComponentModel;
using System.Windows.Forms;

namespace SimuLoad.Views;

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
        closeBtn.Location = new System.Drawing.Point(500, 274);
        closeBtn.Name = "closeBtn";
        closeBtn.Size = new System.Drawing.Size(75, 23);
        closeBtn.TabIndex = 1;
        closeBtn.Text = "Close";
        closeBtn.UseVisualStyleBackColor = true;
        closeBtn.Click += closeBtn_Click;
        // 
        // tryBtn
        // 
        tryBtn.Location = new System.Drawing.Point(215, 274);
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
        // MainForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(587, 309);
        Controls.Add(disBtn);
        Controls.Add(tryBtn);
        Controls.Add(closeBtn);
        Controls.Add(debugBox);
        Name = "MainForm";
        Text = "MainForm";
        Load += MainForm_Load;
        ResumeLayout(false);
    }

    #endregion

    private ListBox debugBox;
    private Button closeBtn;
    private Button tryBtn;
    private Button disBtn;
}
