using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSU_Calculator.Komponenten
{
  public class PowerSupply : PcComponent
  {
      public PowerSupply(string _name, int _power, int _toTDP, string _ghLink)
        :this (_name,_power,_toTDP,_ghLink,50)  
    {
    }

    public PowerSupply(string _name, int _power, int _toTDP, string _ghLink, int qualitaet)
      : base(_name, _toTDP, _power)
    {
      Geizhals = _ghLink;
      Quality = qualitaet;
    }

    public string Geizhals
    {
      get;
      set;
    }

    public int UsageLoadMaximum
    {
      get
      {
        return GPGPU;
      }
    }


    public override string GetOrginalString()
    {
      return string.Format("{0};{1};{2};{3};{4}", Name, TDP, UsageLoadMaximum, Quality, Geizhals);
    }

    public int Quality
    {
      set;
      get;
    }
  }
}
