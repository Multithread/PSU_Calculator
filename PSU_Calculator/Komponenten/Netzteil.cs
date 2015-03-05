using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSU_Calculator.Komponenten
{
  public class Netzteil : PcKomponente
  {
      public Netzteil(string _name, int _power, int _toTDP, string _ghLink)
        :this (_name,_power,_toTDP,_ghLink,50)  
    {
    }

    public Netzteil(string _name, int _power, int _toTDP, string _ghLink, int qualitaet)
      : base(_name, _toTDP, _power)
    {
      Geizhals = _ghLink;
      Qualitaet = qualitaet;
    }

    public string Geizhals
    {
      get;
      set;
    }

    public int BesteAuslastung
    {
      get
      {
        return GPGPU;
      }
    }


    public virtual string GetOrginalString()
    {
      return string.Format("{0};{1};{2};{3};{4}", Bezeichnung, TDP, BesteAuslastung, Qualitaet, Geizhals);
    }

    public int Qualitaet
    {
      set;
      get;
    }
  }
}
