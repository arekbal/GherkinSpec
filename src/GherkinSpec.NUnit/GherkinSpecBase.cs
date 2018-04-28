using GherkinSpec.Core;
using System;
using GherkinSpec.Core.Out;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace GherkinSpec.NUnit
{
  [TestFixture]
  public class GherkinSpecBase : InternalGherkinSpecBase
  {
    GherkinSpecContext _specContext;

    protected override sealed GherkinSpecContext GetSpecContext()
    {
      return _specContext;
    }

    public TestContext TestContext => TestContext.CurrentContext;

    protected GherkinSpecBase()
    {      
    }
    
    [SetUp]
    public void TestSetUp()
    {        
      _specContext = CreateSpecContext();

      try
      {
        // forced so feature related state is processed per test
        _specContext.InitFeature(this, TestContext.CurrentContext.TestDirectory);

        Background();

        OnInitScenario();

        _specContext.InitScenario(TestContext.Test.Name);        
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

    protected virtual GherkinSpecContext CreateSpecContext() => new NUnitGherkinSpecContext(CreateFeatureOutput);

    [TearDown]
    public void TestCleanup()
    {
      var testpassed = TestContext.Result.Outcome.Status == TestStatus.Passed;

      OnCleanupScenario(testpassed);

      _specContext.CleanupScenario(testpassed);

      // forced so feature related state is processed per test
      _specContext.CleanupFeature();
    }
  }
}
