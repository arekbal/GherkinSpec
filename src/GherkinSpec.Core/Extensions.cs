using Gherkin.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GherkinSpec.Core
{
  static class Extensions
  {
    public static Background GetBackgroundOrNull(this Feature that)
    {
      foreach (var child in that.Children)
        if (child is Background bg)
          return bg;

      return null;
    }

    public static IEnumerable<Tag> GetTags(this ScenarioDefinition that)
    {
      if (that is Scenario scenario)
        return scenario.Tags;

      if (that is ScenarioOutline scenarioDef)
        return scenarioDef.Tags;

      return Enumerable.Empty<Tag>();
    }
  }
}
