using doix.gherkin_spec.core.Meta;
using System;

namespace doix.gherkin_spec.nunit.Meta
{
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
  public sealed class ScenarioAttribute : Attribute, IScenarioAttribute
  {
    public string ScenarioName { get; }

    public ScenarioAttribute(string scenarioName = null)
    {
      ScenarioName = scenarioName;
    }
  }
}
