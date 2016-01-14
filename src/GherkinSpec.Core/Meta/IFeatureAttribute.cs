using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinSpec.Core.Meta
{
  public interface IFeatureAttribute
  {
    Type Loader { get; set; }
    string LoaderParam { get; set; }
  }
}
