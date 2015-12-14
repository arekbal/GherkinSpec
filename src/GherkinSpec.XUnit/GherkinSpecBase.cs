using System.Collections.Generic;
using GherkinSpec.Core;
using System;
using GherkinSpec.Core.Out;
using Xunit.Abstractions;
using Xunit;
using System.Linq;
using System.Threading;

namespace GherkinSpec.XUnit
{ 
  public class GherkinSpecBase : IDisposable, IXUnitGherkinSpecContextProvider, IClassFixture<XUnitGherkinSpecContext>
  {
    readonly XUnitGherkinSpecContext _specContext;

    XUnitGherkinSpecContext IXUnitGherkinSpecContextProvider.SpecContext => _specContext;

    static AsyncLocal<GherkinSpecBase> _current = new AsyncLocal<GherkinSpecBase>();

    public static GherkinSpecBase Curr => _current.Value;

    protected GherkinSpecBase(XUnitGherkinSpecContext specContext)
    {
      var attr = (XUnitFeatureAttribute)GetType().GetCustomAttributes(typeof(XUnitFeatureAttribute), true).FirstOrDefault();

      //attr.B

      _current.Value = this;

      _specContext = specContext;
      _specContext.InitFeature(this);
      _specContext.Background();
      //_specContext.InitScenario("");
    }

    protected IEnumerable<string> Tags => _specContext.Tags;  

    public virtual void Background()
    {
    }

    public void Step(string textStartingWithKeyword) => _specContext.Step(textStartingWithKeyword);

    protected void Step(string keyword, string text) => _specContext.Step(keyword, text);

    public void Given(string precondition) => Step(nameof(Given), precondition);
  
    public void When(string precondition) => Step(nameof(When), precondition);

    public void Then(string precondition) => Step(nameof(Then), precondition);

    public void And(string precondition) => Step(nameof(And), precondition);

    public void But(string precondition) => Step(nameof(But), precondition);

    public void Dispose()
    {
      _specContext.CleanupScenario(true);
      _specContext.CleanupFeature();
    }

    public IEnumerable<IReadOnlyDictionary<string, string>> ArgumentTable => _specContext.ArgumentTable;

    public IEnumerable<string> ArgumentList => _specContext.ArgumentList;

    public IEnumerable<IReadOnlyDictionary<string, string>> ResultTable => _specContext.ArgumentTable;

    public IEnumerable<string> ResultList => _specContext.ArgumentList;

    public ExampleSets ExampleSets => _specContext.ExampleSets;

    public bool AllScenariosCovered => _specContext.AllScenariosCovered;

    
  }
}
