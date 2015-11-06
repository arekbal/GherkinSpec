using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gherkin.Ast;
using System.IO;

namespace GherkinSpec.Core.FeatureLoaders
{
  public class FeatureFileLoader : IFeatureLoader
  {
    public TextReader ReadFeatureContent(string filePath) => new StreamReader(filePath); 
  }
}
