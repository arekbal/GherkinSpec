using GherkinSpec.Core;
using GherkinSpec.Core.Meta;
using GherkinSpec.XUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;


namespace GherkinSpec.Tests
{
  [Feature(FilePath="Feature.feature")]
  [XUnitFeature]
  public class XUnitTests : GherkinSpecBase
  {
    public XUnitTests(ITestOutputHelper output) 
      : base(new XUnitGherkinSpecContext(output))
    {
    }

    [XUnitScenario(DisplayName = "Passing Test")]
    public void PassingTest()
    {
      //output.WriteLine("Hello");

      Assert.Equal(4, Add(2, 2));
    }

    [Fact(DisplayName= "Failing Test")]
    public void FailingTest()
    {
      Assert.Equal(5, Add(2, 2));
    }

    int Add(int x, int y)
    {
      return x + y;
    }
  }
}
