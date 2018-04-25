using doix.gherkin_spec.core;
using System;
using doix.gherkin_spec.core.Out;

namespace doix.gherkin_spec.nunit
{
  public class NUnitGherkinSpecContext : GherkinSpecContext
  {
    readonly Func<IFeatureOutput> _outputFactory;

    public NUnitGherkinSpecContext(Func<IFeatureOutput> outputFactory)
    {
      _outputFactory = outputFactory;
    }

    protected override IFeatureOutput CreateOutput()
    {
      return _outputFactory();
    }
  }
}
