using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using GherkinSpec.Core;
using System;

namespace GherkinSpec.MsTest
{
  [TestClass]
  public class GherkinSpecBase
  {
    static GherkinSpecContext Ctx;

    public TestContext TestContext { get; set; }

    static bool _isStaticInitialized;

    protected GherkinSpecBase()
    {
      if(!_isStaticInitialized)
      {
        Ctx = new GherkinSpecContext();

        _isStaticInitialized = true;
      }
    }   

    [TestInitialize]
    public void InitTest()
    {
      try
      {
        // forced so feature related state is processed per test
        Ctx.InitFeature(this);

        Background();

        Ctx.InitScenario(TestContext.TestName);

        OnInitTest();
      }
      catch (Exception ex)
      {
        CleanupTest();
        throw;
      }
    }

    public virtual void OnInitTest()
    {
    }

    public virtual void OnCleanupTest()
    {
    }

    public virtual void Background()
    {
    }

    [TestCleanup]
    public void CleanupTest()
    {
      OnCleanupTest();

      Ctx.CleanupScenario(TestContext.CurrentTestOutcome == UnitTestOutcome.Passed);

      // forced so feature related state is processed per test
      Ctx.CleanupFeature();
    }

    public void Step(string textStartingWithKeyword) => Ctx.Step(textStartingWithKeyword);

    protected static void Step(string keyword, string text) => Ctx.Step(keyword, text);

    public void Given(string precondition) => Step(nameof(Given), precondition);
  
    public void When(string precondition) => Step(nameof(When), precondition);

    public void Then(string precondition) => Step(nameof(Then), precondition);

    public void And(string precondition) => Step(nameof(And), precondition);

    public void But(string precondition) => Step(nameof(But), precondition);

    public IEnumerable<IReadOnlyDictionary<string, string>> ArgumentTable => Ctx.ArgumentTable;

    public IEnumerable<string> ArgumentList => Ctx.ArgumentList;

    public IEnumerable<IReadOnlyDictionary<string, string>> ResultTable => Ctx.ArgumentTable;

    public IEnumerable<string> ResultList => Ctx.ArgumentList;

    public ExampleSets ExampleSets => Ctx.ExampleSets;

    public bool AllScenariosCovered => Ctx.AllScenariosCovered;
  }
}
