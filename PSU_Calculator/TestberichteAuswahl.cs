using PSU_Calculator.Komponenten;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSU_Calculator
{
  public partial class TestberichteAuswahl : Form
  {
    PowerSupply PSU;
    public TestberichteAuswahl(PowerSupply psu)
    {
      PSU = psu;
      InitializeComponent();
      foreach (string link in PSU.Testberichte)
      {
        if (link.ToLower().StartsWith("www."))
        {
          int index = link.IndexOf("/");
          if (index > -1)
          {
            string show = link.Substring(0, index);
            cbxTestberichte.Items.Add(new NTLinkHelper(show, link));
            continue;
          }
        }
        int side = Get3rdIndex(link);
        if (side == -1)
        {
          cbxTestberichte.Items.Add(new NTLinkHelper(link, link));
        }
        else
        {
          string show = link.Substring(link.IndexOf('/') + 2, side - link.IndexOf('/') - 2);
          cbxTestberichte.Items.Add(new NTLinkHelper(show,link));
        }
      }
      cbxTestberichte.SelectedIndex = 0;
    }

    private void cmdShowTest_Click(object sender, EventArgs e)
    {
      NTLinkHelper bericht = (NTLinkHelper)cbxTestberichte.SelectedItem;
      System.Diagnostics.Process.Start(bericht.Link);
    }

    protected class NTLinkHelper
    {
      public NTLinkHelper(string show, string link)
      {
        Show = show;
        Link = link;
      }

      private string Show = "";
      public string Link
      {
        get;
        set;
      }

      public override string ToString()
      {
        return Show;
      }
    }

    public int Get3rdIndex(string s)
    {
      char t = '/';
      int n = 3;
      int count = 0;
      char[] chars = s.ToCharArray();
      for (int i = 0; i < chars.Length; i++)
      {
        if (chars[i] == t)
        {
          count++;
          if (count == n)
          {
            return i;
          }
        }
      }
      return -1;
    }
  }
}
