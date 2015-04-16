using PSU_Calculator.DataWorker;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSU_Calculator.Komponenten
{
  public class ShowableComponent : PcComponent, IComparable<ShowableComponent>
  {
    private CheckBox check = null;
    private ComboBox combo = null;
    private Panel panel = null;
    private Label label = null;
    private static int TabIndex = 100;

    /// <summary>
    /// Control für die Anzeige
    /// </summary>
    public Control Control
    {
      get
      {
        if (panel != null)
        {
          return panel;
        }
        if (combo != null)
        {
          return combo;
        }
        if (check != null)
        {
          return check;
        }
        if (label != null)
        {
          return label;
        }
        return null;
      }
    }

    /// <summary>
    /// Controll für die berechnung der Leistungsaufnahme
    /// </summary>
    public Control DataContainerControl
    {
      get
      {
        if (combo != null)
        {
          return combo;
        }
        if (check != null)
        {
          return check;
        }
        return null;
      }
    }

    public int TitleOrder
    {
      get;
      set;
    }

    public int Order
    {
      get;
      set;
    }

    public int Row
    {
      get;
      set;
    }

    public int Width
    {
      get;
      set;
    }

    public ShowableComponent(Element ele)
      : base(ele)
    {
      TitleOrder = getIntForString(ele.getAttribut("TitleOrder"));
      Order = getIntForString(ele.getAttribut("Order"));
      Row = getIntForString(ele.getAttribut("Row"));
      Width = getIntForString(ele.getAttribut("Width"));

      if (Width == 0)
      {
        Width = 200;
      }

      if ("CheckBox".Equals(this.Type))
      {
        check = new CheckBox();
        check.Checked = false;
        check.Text = Name;
        check.Name = Name;
        check.Size = new Size(Width, 20);
        check.Location = new Point(5, 20);
        check.AutoSize = true;
        check.Tag = new PcComponent(Name, TDP, "None");
        //check.Tag = this;
        check.TabIndex = TabIndex++;
      }
      else if ("DropDown".Equals(this.Type))
      {
        //Label generieren
        label = new Label();
        label.Text = Name;
        label.Size = new Size(Width / 2, 25);
        label.Location = new Point(0, 7);
        using (Graphics g = label.CreateGraphics())
        {
          SizeF size = g.MeasureString(label.Text, label.Font, 495);
          label.Height = (int)Math.Ceiling(size.Height);
          label.Width = (int)Math.Ceiling(size.Width);
        }

        combo = new ComboBox();
        combo.Text = Name;
        combo.Name = Name;
        combo.Size = new Size(Width - label.Width - 12, 25);
        combo.TabIndex = TabIndex++;
        combo.Location = new Point(label.Width + 8, 5);

        //Componenten Setzen
        PcComponentList components;
        if (!string.IsNullOrEmpty(ele.getAttribut("DataList")))
        {
          components = new PcComponentList(LoaderModul.getInstance().GetComponentsFromFile(ele.getAttribut("DataList")));
          combo.SelectedIndex = 0;
        }
        else
        {
          components = new PcComponentList(new List<PcComponent>());
          int amount = getIntForString(ele.getAttribut("Amount"));
          for (int i = amount; i >= 0; i--)
          {
            PcComponent com = new PcComponent(i.ToString(), TDP * i, Name);
            components.Components.Add(com);
          }
        }
        combo.Tag = components;
        combo.Items.AddRange(components.Components.ToArray());
        combo.SelectedIndex = combo.Items.Count - 1;

        panel = new Panel();
        panel.Size = new Size(Width, 29);
        
        panel.Controls.Add(label);
        panel.Controls.Add(combo);
      }
      else if ("Label".Equals(this.Type))
      {
        label = new Label();
        label.Text = Name;
        label.Name = Name;
        label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        label.ForeColor = System.Drawing.Color.Black;
        label.Size = new Size(Width, 25);
      }
    }

    public int CompareTo(ShowableComponent other)
    {
      if (other.Row != this.Row)
      {
        return this.Row.CompareTo(other.Row);
      }
      if (other.TitleOrder != this.TitleOrder)
      {
        return this.TitleOrder.CompareTo(other.TitleOrder);
      }
      return this.Order.CompareTo(other.Order);
    }
  }
}
