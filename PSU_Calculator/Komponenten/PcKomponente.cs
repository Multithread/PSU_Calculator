using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSU_Calculator.Komponenten
{
  public class PcComponent
  {
    public PcComponent(string name, int tdp, int benchmarking)
    {
      Name = name;
      TDP = tdp;
      GPGPU = benchmarking;
    }

    protected PcComponent()
    {
      Name = "";
      TDP = 0;
      GPGPU= 0;
    }

    public string Name
    {
      get;
      set;
    }

    public virtual int TDP
    {
      get;
      set;
    }

    public int GPGPU
    {
      get;
      set;
    }

    public virtual string GetOrginalString()
    {
      return string.Format("{0}:{1}:{2}", Name, TDP, GPGPU);
    }

    public override string ToString()
    {
      return Name;
    }
  }
}
