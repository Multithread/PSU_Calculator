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

    public ShowableComponent(Element ele)
      : base(ele)
    {
      TitleOrder = getIntForString(ele.getAttribut("TitleOrder"));
      Order = getIntForString(ele.getAttribut("Order"));

      if ("CheckBox".Equals(this.Type))
      {
        check = new CheckBox();
        check.Checked = false;
        check.Text = Name;
        check.Name = Name;
        check.Size = new Size(200, 17);
        check.AutoSize = true;
        check.TabIndex = TabIndex++;
      }
      else if ("DropDown".Equals(this.Type))
      {
        int amount = getIntForString(ele.getAttribut("Amount")); 
        combo = new ComboBox();
        combo.Text = Name;
        combo.Name = Name;
        combo.Size = new Size(70, 25);
        combo.TabIndex = TabIndex++;
        combo.Location = new Point(100, 5);
        for (int i = amount; i >= 0; i--)
        {
          PcComponent com = new PcComponent(i.ToString(), TDP * i, Name);
          combo.Items.Add(com);
        }
        combo.SelectedIndex = amount;

        panel = new Panel();
        panel.Size = new Size(200, 25);

        label = new Label();
        label.Text = Name;
        label.Size = new Size(100, 25);
        label.Location = new Point(0,10);

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
        label.Size = new Size(200, 25);
      }
    }

    public int CompareTo(ShowableComponent other)
    {
      if (other.TitleOrder != this.TitleOrder)
      {
        return this.TitleOrder.CompareTo(other.TitleOrder);
      }
      return this.Order.CompareTo(other.Order);
    }
  }
}
