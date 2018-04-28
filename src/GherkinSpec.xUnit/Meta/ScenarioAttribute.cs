using GherkinSpec.Core.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace GherkinSpec.xUnit
{
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
  public class ScenarioAttribute : FactAttribute, IScenarioAttribute
  {
    public string ScenarioName
    {
      get
      {
        return DisplayName;
      }
      set
      {
        DisplayName = value;
      }
    }
  }
}
