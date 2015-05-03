using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSU_Calculator.Komponenten
{
  [DebuggerDisplay("Lenght={Components.Count}")]
  public class PcComponentList
  {
    public PcComponentList(List<PcComponent> components)
    {
      Components = components;
    }

    public List<PcComponent> Components
    {
      get;
      set;
    }

    public string searchQuery = "";

    public string ManageKey(KeyEventArgs args)
    {
      if (!char.IsLetterOrDigit((char)args.KeyValue) && !Keys.Back.Equals(args.KeyCode) && !Keys.Delete.Equals(args.KeyCode) && !Keys.NumPad0.Equals(args.KeyCode))
      {
        if (Keys.Delete.Equals(args.KeyCode))
        {
          if (!string.IsNullOrEmpty(searchQuery))
          {
            searchQuery.Remove(searchQuery.Length - 1);
          }
        }
        else
          if (Keys.Back.Equals(args.KeyCode))
          {
            searchQuery = "";
          }
      }
      else
      {
        searchQuery += (char)args.KeyValue;
      }
      return searchQuery;
    }
  }
}
