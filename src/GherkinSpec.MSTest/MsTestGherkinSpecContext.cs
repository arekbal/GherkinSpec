using GherkinSpec.Core;
using System;
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
