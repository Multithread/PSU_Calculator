using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSU_Calculator.Komponenten
{
  class CoolingSolution : PcComponent
  {
    public CoolingSolution(string _bez, int _tdp, bool _gpu, CoolingTyp _coolingsolution, bool _onlyOnce)
      : base(_bez, _tdp,"Cooling")
    {
      GPU = _gpu;
      CoolingTypTyp = _coolingsolution;
      OnlyOnce = _onlyOnce;
    }

    public CoolingSolution(string _bez, int _tdp, bool _gpu, CoolingTyp _coolingsolution)
      : this(_bez, _tdp,_gpu, _coolingsolution, false)
    {
    }

    public void SetOnlyOnce(bool _value)
    {
      OnlyOnce = _value;
    }

    public CoolingTyp CoolingTypTyp
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
      Air = 30,
      Water = 50,
      LN2 = 100
    }
  }
}
