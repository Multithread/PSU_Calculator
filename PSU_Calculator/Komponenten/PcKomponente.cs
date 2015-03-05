using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSU_Calculator.Komponenten
{
  public class PcKomponente
  {
    public PcKomponente(string _bez, int _tdp, int _benchmarking)
    {
      Bezeichnung = _bez;
      TDP = _tdp;
      GPGPU = _benchmarking;
    }

    public string Bezeichnung
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
      return string.Format("{0}:{1}:{2}", Bezeichnung, TDP, GPGPU);
    }

    public override string ToString()
    {
      return Bezeichnung;
    }
  }
}
