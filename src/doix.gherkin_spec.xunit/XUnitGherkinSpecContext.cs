using System;
using Xunit.Abstractions;
using Xunit;
using System.Threading;
using doix.gherkin_spec.core.Out;
using doix.gherkin_spec.core;

namespace doix.gherkin_spec.xunit
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
  }
}
