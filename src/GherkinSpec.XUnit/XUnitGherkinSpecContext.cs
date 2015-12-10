using GherkinSpec.Core;
using System;
using GherkinSpec.Core.Out;
using Xunit.Abstractions;
using Xunit;

namespace GherkinSpec.XUnit
{
  public class XUnitGherkinSpecContext : GherkinSpecContext
  {
    readonly ITestOutputHelper _output;

    public XUnitGherkinSpecContext(ITestOutputHelper output)
    {
      _output = output;
    }

    protected override IFeatureOutput CreateOutput()
    {
      return new XUnitFeatureOutput(_output);
    }

    public virtual void Background()
    {
    }
  }
}
