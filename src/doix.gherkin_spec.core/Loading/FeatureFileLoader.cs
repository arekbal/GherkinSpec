using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gherkin.Ast;
using System.IO;

namespace doix.gherkin_spec.core.Loading
{
  public class FeatureFileLoader : IFeatureLoader
  {
    public TextReader ReadFeatureContent(string filePath, object testContainer, string testDirectory) 
      => new StreamReader(Path.Combine(testDirectory, filePath));
  }
}
