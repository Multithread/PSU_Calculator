using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSU_Calculator.Komponenten
{
  class Kuehlung : PcKomponente
  {
    public Kuehlung(string _bez, int _tdp, bool _gpu, CoolingTyp _kuehlloesung, bool _onlyOnce)
      : base(_bez, _tdp, _tdp)
    {
      GPU = _gpu;
      Kuehlloesung = _kuehlloesung;
      OnlyOnce = _onlyOnce;
    }

    public Kuehlung(string _bez, int _tdp, bool _gpu, CoolingTyp _kuehlloesung)
      : this(_bez, _tdp,_gpu, _kuehlloesung, false)
    {
    }

    public void setOnlyOnce(bool _value)
    {
      OnlyOnce = _value;
    }

    public CoolingTyp Kuehlloesung
    {
      get;
      set;
    }

    public bool GPU
    {
      get;
      set;
    }

    public bool OnlyOnce
    {
      get;
      set;
    }

    public enum CoolingTyp
    {
      Luft = 30,
      Wasser = 50,
      LN2 = 100
    }
  }
}
