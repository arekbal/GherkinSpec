using Gherkin.Ast;
using GherkinSpec.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.String;

namespace GherkinSpec.Core.FeatureLoaders
{
  static class FeatureLoaderSelector
  {
    public static Conditional<Feature> Select(FeatureAttribute attr)
    {
      var parser = new Gherkin.Parser();

      if (!IsNullOrWhiteSpace(attr.FilePath))
        using (var reader = new FeatureFileLoader().ReadFeatureContent(attr.FilePath))
          return Conditional.From(parser.Parse(reader));

      if (!IsNullOrWhiteSpace(attr.FileAddress))
        using (var reader = new FeatureHttpLoader().ReadFeatureContent(attr.FileAddress))
          return Conditional.From(parser.Parse(reader));

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
