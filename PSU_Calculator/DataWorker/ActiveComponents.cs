using PSU_Calculator.DataWorker.Elementworker;
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
    public List<PowerSupply> LastEmpfohlenePowerSupplys = new List<PowerSupply>();

    public List<ShowableComponent> ShowableComponentList
    {
      get;
      set;
    }

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

    public int GetShowableComponentsHeight()
    {
      int maxHeigh = 0;
      maxHeigh = Math.Max(maxHeigh, ShowableHeight(1));
      maxHeigh = Math.Max(maxHeigh, ShowableHeight(2));
      maxHeigh = Math.Max(maxHeigh, ShowableHeight(3));
      return maxHeigh;
    }

    private int ShowableHeight(int row)
    {
      if (ShowableComponentList == null)
      {
        return 0;
      }
      int posFromTop = 0;
      foreach (ShowableComponent tmpSc in ShowableComponentList)
      {
        if (tmpSc.Row != row)
        {
          continue;
        }
        tmpSc.Control.Location = new System.Drawing.Point(tmpSc.Control.Location.X, posFromTop);
        posFromTop += tmpSc.Control.Size.Height;
      }
      return posFromTop;
    }

    public bool AddControl(Control inCbx)
    {
      if (inCbx == null)
      {
        return false;
      }
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
      if (inCbx is CheckBox)
      {
        (inCbx as CheckBox).CheckedChanged += Control_Changed;
      }
      else if (inCbx is ComboBox)
      {
        (inCbx as ComboBox).SelectedIndexChanged += Control_Changed;
      }
      return true;
    }

    void Control_Changed(object sender, EventArgs e)
    {
      if (ActiveComponentChangedEvent != null)
      {
        ActiveComponentChangedEvent(sender);
      }
    }
    public delegate void ActiveComponentUpdateDelegate(object sender);
    public event ActiveComponentUpdateDelegate ActiveComponentChangedEvent;

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

    /// <summary>
    /// Anzahl Stecker für die Komponenten zusammenzählen.
    /// </summary>
    /// <returns></returns>
    public Stecker GetNeededStecker()
    {
      Element ele = new Element("Stecker");
      int PCIE8 = 0;
      int PCIE6 = 0;
      int Molex = 0;
      int Sata = 0;

      foreach (PcComponent com in GetAktiveComponents())
      {
        var datacontainer= new ElementDataContainer(com.Data);
        var stecker = datacontainer.Conectors;
        PCIE8 += stecker.PCIE8;
        PCIE6 += stecker.PCIE6;
        Molex += stecker.Molex;
        Sata += stecker.Sata;
      }

      ele.addAttribut(Stecker.SteckerType.PCIE8.ToString(), PCIE8.ToString());
      ele.addAttribut(Stecker.SteckerType.PCIE6.ToString(), PCIE6.ToString());
      ele.addAttribut(Stecker.SteckerType.Molex.ToString(), Molex.ToString());
      ele.addAttribut(Stecker.SteckerType.Sata.ToString(), Sata.ToString());
      return new Stecker(ele);
    }

    /// <summary>
    /// Netzteile für den Rechner empfehlen
    /// </summary>
    /// <param name="Watt"></param>
    public List<PowerSupply> EmpfohleneNetzteile()
    {
      return EmpfohleneNetzteile(-1);
    }

    private int getGPUWattage()
    {
      int wattage = 0;
      foreach (PcComponent com in GetAktiveComponents())
      {
        if ("GPU".Equals(com.Type))
        {
          wattage += com.TDP;
        }
      }
      return wattage;
    }

    /// <summary>
    /// Netzteile für den Rechner empfehlen
    /// </summary>
    /// <param name="Watt"></param>
    public List<PowerSupply> EmpfohleneNetzteile(int Watt)
    {
      if (Watt == -1)
      {
        Watt = GetWattage();
      }
      int gpuWattage= getGPUWattage();

      Stecker neededConectors = ActiveComponents.Get().GetNeededStecker();
      ElementDataContainer psuDataContainer;
      List<PowerSupply> empfehlenswerte = new List<PowerSupply>();
      foreach (PowerSupply nt in LoaderModul.getInstance().GetPowerSupplys())
      {
        if (nt.UsageLoadMaximum < Watt)
        {
          continue;
        }
        if (nt.UsageLoadMinimum != -1)
        {
          if (nt.UsageLoadMinimum >= Watt)
          {
            continue;
          }
        }else if (nt.UsageLoadMaximum >= (Watt + 100 + (nt.TDP * 0.1)))
        {
          continue;
        }

        psuDataContainer = nt.DataContainer;
        //Netzteil muss genügend Stecker haben.
        if (PSUCalculatorSettings.Get().ConnectorsHaveToFit)
        {
          if (!psuDataContainer.Conectors.HasMoreOrEqualPlugsAs(neededConectors))
          {
            continue;
          }
        }
        //12V Grafikkartenleistung beachten.
        if (psuDataContainer.GpuEnergy.Watt != 0 && psuDataContainer.GpuEnergy.Watt < gpuWattage)
        {
          gpuWattage++;
          continue;
        }

        //Empfehlen
        empfehlenswerte.Add(nt);
      }
      LastEmpfohlenePowerSupplys = empfehlenswerte;
      return empfehlenswerte;
    }
  }
}
