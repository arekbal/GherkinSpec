using Gherkin.Ast;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doix.gherkin_spec.core.Loading
{
  public interface IFeatureLoader
  {
    TextReader ReadFeatureContent(string virtualPath, object testContainer, string testDirectory);
  }
}
