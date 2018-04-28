using GherkinSpec.Core;
using System;
using GherkinSpec.Core.Out;

namespace GherkinSpec.NUnit
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
