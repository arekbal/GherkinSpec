using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinSpec.Core
{
  public static class ReadOnlyDictionaryExtensions
  {
    public int GetInt(this IReadOnlyDictionary<string, string> that, string key)
    {
      return Convert.ToInt32(that[key]);
    }
  }
}
