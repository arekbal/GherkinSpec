using GherkinSpec.Core;
using System;
using GherkinSpec.Core.Out;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GherkinSpec.MsTest
{
  [TestClass]
  public class GherkinSpecBase : InternalGherkinSpecBase
  {
    GherkinSpecContext _specContext;

    protected override sealed GherkinSpecContext GetSpecContext()
    {
      return _specContext;
    }

    public TestContext TestContext { get; set; }

    protected GherkinSpecBase()
    {      
    }
    
    [TestInitialize]
    public void TestInitialize()
    {        
      _specContext = CreateSpecContext();

      try
      {
        // forced so feature related state is processed per test
        _specContext.InitFeature(this);

        Background();

        OnInitScenario();
     
        _specContext.InitScenario(TestContext.TestName);        
      }
      catch (Exception)
      {
        TestCleanup();
        throw;
      }
    }

    protected virtual IFeatureOutput CreateFeatureOutput()
    {
      return new FeatureConsoleOutput();
    }

    protected virtual GherkinSpecContext CreateSpecContext() => new MsTestGherkinSpecContext(CreateFeatureOutput);

    [TestCleanup]
    public void TestCleanup()
    {
      var testpassed = TestContext.CurrentTestOutcome == UnitTestOutcome.Passed;

      OnCleanupScenario(testpassed);

      _specContext.CleanupScenario(testpassed);

      // forced so feature related state is processed per test
      _specContext.CleanupFeature();
    }
  }
}
