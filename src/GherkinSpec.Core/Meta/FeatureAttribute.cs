using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinSpec.Core.Meta
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public sealed class FeatureAttribute : Attribute
  {
    public Type Loader { get; set; }
    public string LoaderParam { get; set; }
    public string FileAddress { get; set; }
    public string FilePath { get; set; }
  }
}
