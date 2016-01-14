using Gherkin.Ast;
using GherkinSpec.Core.Meta;
using GherkinSpec.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.String;

namespace GherkinSpec.Core.Loading
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
