using PSU_Calculator.Komponenten;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PSU_Calculator
{
  /// <summary>
  /// Einfaches Input Feld, mit übernehmen Button und Regex Prüfung.
  /// </summary>
  public partial class InputFeld : Form
  {
    PowerSupply PSU;
    private Regex myRegex;
    public InputFeld(string inTitle, Regex inRegex)
    {
      InitializeComponent();
      Name = inTitle;
      myRegex = inRegex;
      FormClosing += InputFeld_FormClosing;
    }

    void InputFeld_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (string.IsNullOrWhiteSpace(tbxInput.Text))
      {
        this.DialogResult = DialogResult.Cancel;
      }
      if (myRegex !=null)
      {
        if (!myRegex.IsMatch(tbxInput.Text))
        {
          this.DialogResult = DialogResult.Cancel;
        }
      }
    }

    public string GetText
    {
      get
      {
        return tbxInput.Text;
      }
      set
      {
        tbxInput.Text = value;
      }
    }

    private void Finished(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
      this.Close();
    }


  }
}
