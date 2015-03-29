namespace PSU_Calculator
{
  partial class About
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
      this.LinkLabel2 = new System.Windows.Forms.LinkLabel();
      this.Button1 = new System.Windows.Forms.Button();
      this.Label3 = new System.Windows.Forms.Label();
      this.LinkLabel1 = new System.Windows.Forms.LinkLabel();
      this.Label1 = new System.Windows.Forms.Label();
      this.Button2 = new System.Windows.Forms.Button();
      this.RichTextBox1 = new System.Windows.Forms.RichTextBox();
      this.Label2 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // LinkLabel2
      // 
      this.LinkLabel2.AutoSize = true;
      this.LinkLabel2.Location = new System.Drawing.Point(15, 199);
      this.LinkLabel2.Name = "LinkLabel2";
      this.LinkLabel2.Size = new System.Drawing.Size(89, 13);
      this.LinkLabel2.TabIndex = 50;
      this.LinkLabel2.TabStop = true;
      this.LinkLabel2.Text = "Weitere Software";
      this.LinkLabel2.Visible = false;
      // 
      // Button1
      // 
      this.Button1.Location = new System.Drawing.Point(18, 113);
      this.Button1.Name = "Button1";
      this.Button1.Size = new System.Drawing.Size(250, 23);
      this.Button1.TabIndex = 49;
      this.Button1.Text = "Update Herunterladen";
      this.Button1.UseVisualStyleBackColor = true;
      this.Button1.Visible = false;
      // 
      // Label3
      // 
      this.Label3.AutoSize = true;
      this.Label3.Location = new System.Drawing.Point(15, 78);
      this.Label3.Name = "Label3";
      this.Label3.Size = new System.Drawing.Size(0, 13);
      this.Label3.TabIndex = 48;
      // 
      // LinkLabel1
      // 
      this.LinkLabel1.AutoSize = true;
      this.LinkLabel1.Location = new System.Drawing.Point(106, 42);
      this.LinkLabel1.Name = "LinkLabel1";
      this.LinkLabel1.Size = new System.Drawing.Size(108, 13);
      this.LinkLabel1.TabIndex = 47;
      this.LinkLabel1.TabStop = true;
      this.LinkLabel1.Text = "Suche nach Updates";
      this.LinkLabel1.Visible = false;
      // 
      // Label1
      // 
      this.Label1.AutoSize = true;
      this.Label1.BackColor = System.Drawing.Color.Transparent;
      this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Label1.Location = new System.Drawing.Point(12, 9);
      this.Label1.Name = "Label1";
      this.Label1.Size = new System.Drawing.Size(202, 31);
      this.Label1.TabIndex = 43;
      this.Label1.Text = "PSU-Calculator";
      // 
      // Button2
      // 
      this.Button2.Location = new System.Drawing.Point(276, 190);
      this.Button2.Name = "Button2";
      this.Button2.Size = new System.Drawing.Size(277, 23);
      this.Button2.TabIndex = 45;
      this.Button2.Text = "Ok";
      this.Button2.UseVisualStyleBackColor = true;
      this.Button2.Click += new System.EventHandler(this.Button2_Click);
      // 
      // RichTextBox1
      // 
      this.RichTextBox1.Location = new System.Drawing.Point(276, 15);
      this.RichTextBox1.Name = "RichTextBox1";
      this.RichTextBox1.ReadOnly = true;
      this.RichTextBox1.Size = new System.Drawing.Size(277, 171);
      this.RichTextBox1.TabIndex = 46;
      this.RichTextBox1.Text = resources.GetString("RichTextBox1.Text");
      // 
      // Label2
      // 
      this.Label2.AutoSize = true;
      this.Label2.BackColor = System.Drawing.Color.Transparent;
      this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Label2.Location = new System.Drawing.Point(15, 40);
      this.Label2.Name = "Label2";
      this.Label2.Size = new System.Drawing.Size(24, 15);
      this.Label2.TabIndex = 44;
      this.Label2.Text = "1.5";
      // 
      // About
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Window;
      this.ClientSize = new System.Drawing.Size(568, 227);
      this.Controls.Add(this.LinkLabel2);
      this.Controls.Add(this.Button1);
      this.Controls.Add(this.Label3);
      this.Controls.Add(this.LinkLabel1);
      this.Controls.Add(this.Label1);
      this.Controls.Add(this.Button2);
      this.Controls.Add(this.RichTextBox1);
      this.Controls.Add(this.Label2);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "About";
      this.Padding = new System.Windows.Forms.Padding(9);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "About";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    internal System.Windows.Forms.LinkLabel LinkLabel2;
    internal System.Windows.Forms.Button Button1;
    internal System.Windows.Forms.Label Label3;
    internal System.Windows.Forms.LinkLabel LinkLabel1;
    internal System.Windows.Forms.Label Label1;
    internal System.Windows.Forms.Button Button2;
    internal System.Windows.Forms.RichTextBox RichTextBox1;
    internal System.Windows.Forms.Label Label2;

  }
}
