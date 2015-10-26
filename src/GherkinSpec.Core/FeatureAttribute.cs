using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinSpec.Core
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public sealed class FeatureAttribute : Attribute
  {
    public string FileAddress { get; set; }
    public string FilePath { get; set; }
  }
}
