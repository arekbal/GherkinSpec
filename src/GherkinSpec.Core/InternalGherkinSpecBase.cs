using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinSpec.Core
{
  public abstract class InternalGherkinSpecBase
  {
    protected abstract GherkinSpecContext GetSpecContext();

    protected virtual void Background()
    {
    }

    protected virtual void OnInitScenario()
    {
    }

    protected virtual void OnCleanupScenario(bool testPassed)
    {
    }

    public void Step(string textStartingWithKeyword) => GetSpecContext().Step(textStartingWithKeyword);

    public void Step(string keyword, string text) => GetSpecContext().Step(keyword, text);

    public void Given(string precondition) => Step(nameof(Given), precondition);

    public void When(string precondition) => Step(nameof(When), precondition);

    public void Then(string precondition) => Step(nameof(Then), precondition);

    public void And(string precondition) => Step(nameof(And), precondition);

    public void But(string precondition) => Step(nameof(But), precondition);

    public string ArgumentString => GetSpecContext().ArgumentString;

    public IEnumerable<IReadOnlyDictionary<string, string>> ArgumentTable => GetSpecContext().ArgumentTable;

    public IEnumerable<string> ArgumentList => GetSpecContext().ArgumentList;

    public IEnumerable<string> Tags => GetSpecContext().Tags;

    public string ResultString => GetSpecContext().ArgumentString;

    public IEnumerable<IReadOnlyDictionary<string, string>> ResultTable => GetSpecContext().ArgumentTable;

    public IEnumerable<string> ResultList => GetSpecContext().ArgumentList;

    public ExampleSets ExampleSets => GetSpecContext().ExampleSets;

    public bool AllScenariosCovered => GetSpecContext().AllScenariosCovered;
  }
}
