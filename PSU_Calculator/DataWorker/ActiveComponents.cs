using PSU_Calculator.Komponenten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSU_Calculator.DataWorker
{
  public class ActiveComponents
  {
    private static ActiveComponents instance = null;
    public static ActiveComponents Get()
    {
      if (instance == null)
      {
        instance = new ActiveComponents();
      }
      return instance;
    }

    private ActiveComponents()
    {
    }

    private List<Control> Controls = new List<Control>();

    public ComboBox CbxCoolingSolution
    {
      get;
      set;
    }

    public ComboBox CbxOC
    {
      get;
      set;
    }

    public bool AddControl(Control inCbx)
    {
      if (inCbx == CbxOC)
      {
        return false;
      }
      if (inCbx == CbxCoolingSolution)
      {
        return false;
      }
      if (Controls.Contains(inCbx))
      {
        return false;
      }
      Controls.Add(inCbx);
      return true;
    }
    public bool RemoveControl(Control c)
    {
      Controls.Remove(c);
      return true;
    }

    public int GetWattage()
    {
      int watt = 20;
      CoolingSolution cooling = CbxCoolingSolution.SelectedItem as CoolingSolution;
      OC overclocking = CbxOC.SelectedItem as OC;

      List<PcComponent> components = GetAktiveComponents();

      bool cpuOcOnce = false;
      foreach (PcComponent com in components)
      {
        if (com.IsEmpty())
        {
          continue;
        }
        if ("CPU".Equals(com.Type))
        {
          if (cpuOcOnce && cooling.OnlyOnce)
          {
          }
          else
          {
            cpuOcOnce = true;
            watt += cooling.TDP;
          }
          watt += overclocking.CalculateCPU_OCUsageInWatt(com.TDP, cooling);
          continue;
        }
        else if ("GPU".Equals(com.Type))
        {
          watt += overclocking.CalculateGPU_OCUsageInWatt(com.TDP, cooling);
          continue;
        }
        if (com.TDP != 0)
        {
          watt += com.TDP;
          continue;
        }
        watt += com.TDP;
      }
      if (!cpuOcOnce && cooling.OnlyOnce)
      {
        watt += cooling.TDP;
      }
      return watt;
    }

    public List<PcComponent> GetAktiveComponents()
    {
      List<PcComponent> components = new List<PcComponent>();
      foreach (Control control in Controls)
      {
        if (control.Visible == false)
        {
          continue;
        }
        components.Add(GetFromControl(control));
      }
      return components;
    }

    /// <summary>
    /// PcComponent aus dem Control auslesen
    /// </summary>
    /// <param name="inControl"></param>
    /// <returns></returns>
    private PcComponent GetFromControl(Control inControl)
    {
      if (inControl is ComboBox)
      {
        return GetFromObject((inControl as ComboBox).SelectedItem);
      }
      if (inControl is CheckBox)
      {
        if (!(inControl as CheckBox).Checked)
        {
          return PcComponent.Empty;
        }
        else
        {
          return GetFromObject(inControl.Tag);
        }
      }
      return GetFromObject(inControl.Tag);
    }

    private PcComponent GetFromObject(object inObj)
    {
      if (inObj == null)
      {
        return PcComponent.Empty;
      }
      PcComponent com = inObj as PcComponent;
      if (com == null)
      {
        return PcComponent.Empty;
      }
      return com;
    }
  }
}
