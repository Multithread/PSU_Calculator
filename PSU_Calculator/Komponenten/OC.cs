using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSU_Calculator.Komponenten
{
  class OC : PcKomponente
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

    public int generateCPU_OCVerbrauch(int watt, Kuehlung kuhl)
    {
      if (!HasCPUOC())
      {
        return watt;
      }
      return watt + (watt * (int)kuhl.Kuehlloesung / 100);
    }

    public int generateGPU_OCVerbrauch(int watt, Kuehlung kuhl)
    {
      if (!HasGPUOC())
      {
        return watt;
      }
      if (kuhl.GPU)
      {
        return watt + (watt * (int)kuhl.Kuehlloesung / 100);
      }
      else
      {
        return watt + (watt * (int)Kuehlung.CoolingTyp.Luft / 100);
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
