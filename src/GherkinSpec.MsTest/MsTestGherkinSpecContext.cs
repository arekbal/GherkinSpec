using GherkinSpec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GherkinSpec.Core.Out;

namespace GherkinSpec.MsTest
{
  public class MsTestGherkinSpecContext : GherkinSpecContext
  {
    readonly Func<IFeatureOutput> _outputFactory;

    public MsTestGherkinSpecContext(Func<IFeatureOutput> outputFactory)
    {
      _outputFactory = outputFactory;
    }

    protected override IFeatureOutput CreateOutput()
    {
      return _outputFactory();
    }
  }
}
