using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSU_Calculator.DataWorker
{
  public class StringJoiner: ComponentStringSplitter
  {
    public StringJoiner(string line, bool toLower)
      : base(line, toLower)
    {
    }

    public StringJoiner()
      : base("", false)
    {
    }

    public void Put(string key, string value)
    {
      if (string.IsNullOrEmpty(key))
      {
        return;
      }
      if (toLoowerString)
      {
        key = key.ToLower();
      }
      if (DataDict.ContainsKey(key))
      {
        DataDict.Remove(key);
      }
      DataDict.Add(key, new List<string>(){value});
    }

    private string Escape(string input)
    {
      input = input.Replace(escape.ToString(), escape.ToString() + escape.ToString());
      input = input.Replace(splitter.ToString(), escape.ToString() + splitter.ToString());
      input = input.Replace(valuesplitter.ToString(), escape.ToString() + valuesplitter.ToString());
      return input;
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      bool notFirst = false;
      foreach (string key in DataDict.Keys)
      {
        List<string> valueList;
        DataDict.TryGetValue(key, out valueList);
        if (ListEmpty(valueList))
        {
          continue;
        }
        if (notFirst)
        {
          sb.Append(splitter);
        }
        sb.Append(Escape(key));
        foreach (string data in valueList)
        {
          sb.Append(valuesplitter);
          sb.Append(Escape(data));
        }
        notFirst = true;
      }

      return sb.ToString();
    }

    private bool ListEmpty(List<string> list) {
      foreach (string s in list)
      {
        if (!string.IsNullOrEmpty(s))
        {
          return false;
        }
      }
      return true;
    }
  }
}
