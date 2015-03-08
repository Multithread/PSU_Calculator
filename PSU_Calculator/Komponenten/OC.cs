using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSU_Calculator.Komponenten
{
  class OC : PcComponent
  {
    public OC(string _bez, bool _isCPU, bool _isGPU)
      : base(_bez, 0, 0)
    {
      CPU = _isCPU;
      GPU = _isGPU;
    }

    public bool HasCPUOC()
    {
      return CPU;
    }
    
    public bool HasGPUOC()
    {
      return GPU;
    }

    public int CalculateCPU_OCUsageInWatt(int _watt, CoolingSolution _cool)
    {
      if (!HasCPUOC())
      {
        return _watt;
      }
      return _watt + (_watt * (int)_cool.CoolingTypTyp / 100);
    }

    public int CalculateGPU_OCUsageInWatt(int _watt, CoolingSolution _cool)
    {
      if (!HasGPUOC())
      {
        return _watt;
      }
      if (_cool.GPU)
      {
        return _watt + (_watt * (int)_cool.CoolingTypTyp / 100);
      }
      else
      {
        return _watt + (_watt * (int)CoolingSolution.CoolingTyp.Air / 100);
      }
    }

    private bool CPU
    {
      get;
      set;
    }

    private bool GPU
    {
      get;
      set;
    }
  }
}
