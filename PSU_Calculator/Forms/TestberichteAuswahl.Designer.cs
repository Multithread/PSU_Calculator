namespace PSU_Calculator
{
  partial class TestberichteAuswahl
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.cbxTestberichte = new System.Windows.Forms.ComboBox();
      this.cmdShowTest = new System.Windows.Forms.Button();
      this.SuspendLayout();
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      // 
      // cbxTestberichte
      // 
      this.cbxTestberichte.FormattingEnabled = true;
      this.cbxTestberichte.Location = new System.Drawing.Point(12, 12);
      this.cbxTestberichte.Name = "cbxTestberichte";
      this.cbxTestberichte.Size = new System.Drawing.Size(267, 21);
      this.cbxTestberichte.TabIndex = 0;
      // 
      // cmdShowTest
      // 
      this.cmdShowTest.Location = new System.Drawing.Point(12, 39);
      this.cmdShowTest.Name = "cmdShowTest";
      this.cmdShowTest.Size = new System.Drawing.Size(138, 25);
      this.cmdShowTest.TabIndex = 1;
      this.cmdShowTest.Text = "Zeige Test";
      this.cmdShowTest.UseVisualStyleBackColor = true;
      this.cmdShowTest.Click += new System.EventHandler(this.cmdShowTest_Click);
      // 
      // TestberichteAuswahl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(293, 77);
      this.Controls.Add(this.cmdShowTest);
      this.Controls.Add(this.cbxTestberichte);
      this.Name = "TestberichteAuswahl";
      this.Text = "TestberichteAuswahl";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ComboBox cbxTestberichte;
    private System.Windows.Forms.Button cmdShowTest;
  }
}