using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using GherkinSpec.Core;
using System;

namespace GherkinSpec.MsTest
{
  [TestClass]
  public class GherkinSpecBase
  {
    static GherkinSpecContext SpecContext;

    public TestContext TestContext { get; set; }

    static bool _isStaticInitialized;

    protected GherkinSpecBase()
    {      
    }

    [TestInitialize]
    public void InitTest()
    {
      if (!_isStaticInitialized)
      {      
        SpecContext = CreateSpecContext();

        _isStaticInitialized = true;
      }

      try
      {
        // forced so feature related state is processed per test
        SpecContext.InitFeature(this);

        Background();

        SpecContext.InitScenario(TestContext.TestName);

        OnInitTest();
      }
      catch (Exception ex)
      {
        CleanupTest();
        throw;
      }
    }

    protected virtual GherkinSpecContext CreateSpecContext() => new GherkinSpecContext();

    protected virtual void OnInitTest()
    {
    }

    protected virtual void OnCleanupTest()
    {
    }

    protected virtual void Background()
    {
    }

    [TestCleanup]
    public void CleanupTest()
    {
      OnCleanupTest();

      SpecContext.CleanupScenario(TestContext.CurrentTestOutcome == UnitTestOutcome.Passed);

      // forced so feature related state is processed per test
      SpecContext.CleanupFeature();
    }

    public void Step(string textStartingWithKeyword) => SpecContext.Step(textStartingWithKeyword);

    protected static void Step(string keyword, string text) => SpecContext.Step(keyword, text);

    public void Given(string precondition) => Step(nameof(Given), precondition);
  
    public void When(string precondition) => Step(nameof(When), precondition);

    public void Then(string precondition) => Step(nameof(Then), precondition);

    public void And(string precondition) => Step(nameof(And), precondition);

    public void But(string precondition) => Step(nameof(But), precondition);

    public IEnumerable<IReadOnlyDictionary<string, string>> ArgumentTable => SpecContext.ArgumentTable;

    public IEnumerable<string> ArgumentList => SpecContext.ArgumentList;

    public IEnumerable<IReadOnlyDictionary<string, string>> ResultTable => SpecContext.ArgumentTable;

    public IEnumerable<string> ResultList => SpecContext.ArgumentList;

    public ExampleSets ExampleSets => SpecContext.ExampleSets;

    public bool AllScenariosCovered => SpecContext.AllScenariosCovered;
  }
}
