using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinSpec.Core.Meta
{
  public interface IScenarioAttribute
  {
    string ScenarioName { get; }
  }
}
