using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSU_Calculator.DataWorker
{
  public class StringJoiner: ComponentStringSplitter
  {
    public StringJoiner()
      : base("", false)
    {
    }

    public void Add(string key, string value)
    {
      DataDict.Add(key, value);
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      bool notFirst = false;
      foreach (string key in DataDict.Keys)
      {
        if (notFirst)
        {
          sb.Append(splitter);
        }
        sb.Append(key);
        sb.Append(valuesplitter);
        string value="";
        DataDict.TryGetValue(key, out value);
        sb.Append(value);
        notFirst = true;
      }

      return sb.ToString();
    }
  }
}
