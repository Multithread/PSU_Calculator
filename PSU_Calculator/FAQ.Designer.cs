namespace PSU_Calculator
{
  partial class FAQ
  {
    /// <summary>
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Vom Windows Form-Designer generierter Code

    /// <summary>
    /// Erforderliche Methode für die Designerunterstützung.
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FAQ));
      this.Button1 = new System.Windows.Forms.Button();
      this.Label30 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // Button1
      // 
      this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.Button1.Location = new System.Drawing.Point(15, 177);
      this.Button1.Name = "Button1";
      this.Button1.Size = new System.Drawing.Size(365, 23);
      this.Button1.TabIndex = 4;
      this.Button1.Text = "Ok";
      this.Button1.UseVisualStyleBackColor = true;
      this.Button1.Click += new System.EventHandler(this.Button1_Click);
      // 
      // Label30
      // 
      this.Label30.AutoSize = true;
      this.Label30.Location = new System.Drawing.Point(12, 9);
      this.Label30.Name = "Label30";
      this.Label30.Size = new System.Drawing.Size(405, 104);
      this.Label30.TabIndex = 3;
      this.Label30.Text = resources.GetString("Label30.Text");
      // 
      // FAQ
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Window;
      this.ClientSize = new System.Drawing.Size(428, 209);
      this.Controls.Add(this.Button1);
      this.Controls.Add(this.Label30);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FAQ";
      this.Padding = new System.Windows.Forms.Padding(9);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "FAQ";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    internal System.Windows.Forms.Button Button1;
    internal System.Windows.Forms.Label Label30;

  }
}
