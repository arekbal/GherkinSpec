using GherkinSpec.Core.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinSpec.MsTest.Meta
{
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
  public sealed class ScenarioAttribute : Attribute, IScenarioAttribute
  {
    public string ScenarioName { get; }

    public ScenarioAttribute(string scenarioName=null)
    {
      ScenarioName = scenarioName;
    }
  }
}
