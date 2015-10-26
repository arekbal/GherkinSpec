using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GherkinSpec
{
  [TestClass]
  public abstract class MsTestBase
  {
    static GherkinSpecContext Ctx;

    public TestContext TestContext { get; set; }

    static bool initialized;

    protected MsTestBase()
    {
      if(!initialized)
      {
        Ctx = new GherkinSpecContext(GetType());

        initialized = true;
      }
    }   

    [TestInitialize]
    public void InitTest()
    {
      Ctx.InitFeature();

      Ctx.InitScenario(TestContext.TestName);

      Background();
    }

    public virtual void Background()
    {
    }

    [TestCleanup]
    public void CleanupTest()
    {
      Ctx.CleanupScenario();
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
  }
}
