using doix.gherkin_spec.core;
using System;
using doix.gherkin_spec.core.Out;

namespace doix.gherkin_spec.mstest
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
