using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doix.gherkin_spec.core.Meta
{
  public interface IFeatureAttribute
  {
    Type Loader { get; set; }
    string LoaderParam { get; set; }
  }
}
