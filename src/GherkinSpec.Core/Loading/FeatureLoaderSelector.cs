using Gherkin.Ast;
using doix.gherkinspec.core.Meta;
using doix.gherkinspec.core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.String;

namespace doix.gherkinspec.core.Loading
{
  static class FeatureLoaderSelector
  {
    public static Conditional<Feature> Select(IFeatureAttribute attr)
    {
      var parser = new Gherkin.Parser();    

      if (attr.Loader.GetInterfaces().Any(t=>t == typeof(IFeatureLoader)))
      {
        var loader = (IFeatureLoader)Activator.CreateInstance(attr.Loader);
        using (var reader = loader.ReadFeatureContent(attr.LoaderParam))
          return Conditional.From(parser.Parse(reader));
      }

      return Conditional<Feature>.Empty;
    }
  }
}
