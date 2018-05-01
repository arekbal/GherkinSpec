using System;
using Xunit.Abstractions;
using Xunit;
using System.Threading;
using GherkinSpec.Core.Out;
using GherkinSpec.Core;

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
  }
}
