using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinSpec.XUnit
{
  static class XUnitFeatureContext
  {
    public static Dictionary<GherkinSpecBase, XUnitGherkinSpecContext> Contexts = new Dictionary<GherkinSpecBase, XUnitGherkinSpecContext>();

    //public static Add(GherkinSpecBase key, XUnitGherkinSpecContext value;)
  }
}
