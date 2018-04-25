using Gherkin.Ast;
using doix.gherkin_spec.core.Meta;
using doix.gherkin_spec.core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.String;

namespace doix.gherkin_spec.core.Loading
{
  static class FeatureLoaderSelector
  {
    public static Conditional<Feature> Select(IFeatureAttribute attr, object testContainer, string testDirectory)
    {
      if (attr.Loader.GetInterfaces().Any(t=>t == typeof(IFeatureLoader)))
      {
        var loader = (IFeatureLoader)Activator.CreateInstance(attr.Loader);
        using (var reader = loader.ReadFeatureContent(attr.LoaderParam, testContainer, testDirectory))
        {
          var parser = new Gherkin.Parser();
          var gherkinDoc = parser.Parse(reader);
          //TODO: gherkinDoc.Comments;
          return Conditional.From(gherkinDoc.Feature);
        }
          
      }

      return Conditional<Feature>.Empty;
    }
  }
}
