using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinSpec.Core
{
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
  public sealed class ScenarioAttribute : Attribute
  {
    public string ScenarioName { get; }

    public ScenarioAttribute(string scenarioName)
    {
      ScenarioName = scenarioName;
    }
  }
}
