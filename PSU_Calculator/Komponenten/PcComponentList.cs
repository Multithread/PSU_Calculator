using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

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
  }
}
