namespace PSU_Calculator
{
  partial class InputFeld
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.cmdShowTest = new System.Windows.Forms.Button();
      this.tbxInput = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // cmdShowTest
      // 
      this.cmdShowTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cmdShowTest.Location = new System.Drawing.Point(12, 48);
      this.cmdShowTest.Name = "cmdShowTest";
      this.cmdShowTest.Size = new System.Drawing.Size(138, 25);
      this.cmdShowTest.TabIndex = 1;
      this.cmdShowTest.Text = "Übernehmen";
      this.cmdShowTest.UseVisualStyleBackColor = true;
      this.cmdShowTest.Click += new System.EventHandler(this.Finished);
      // 
      // tbxInput
      // 
      this.tbxInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbxInput.Location = new System.Drawing.Point(12, 9);
      this.tbxInput.Multiline = true;
      this.tbxInput.Name = "tbxInput";
      this.tbxInput.Size = new System.Drawing.Size(285, 33);
      this.tbxInput.TabIndex = 2;
      // 
      // InputFeld
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(309, 85);
      this.Controls.Add(this.tbxInput);
      this.Controls.Add(this.cmdShowTest);
      this.Name = "InputFeld";
      this.Text = "TestberichteAuswahl";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cmdShowTest;
    private System.Windows.Forms.TextBox tbxInput;
  }
}