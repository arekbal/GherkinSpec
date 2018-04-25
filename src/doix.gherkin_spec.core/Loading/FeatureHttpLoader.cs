using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace doix.gherkin_spec.core.Loading
{
  public class FeatureHttpLoader : IFeatureLoader
  {
    public TextReader ReadFeatureContent(string address, object testContainer, string testDirectory)
    {
      using (var webClient = new WebClient())
        return new StreamReader(webClient.OpenRead(address));
    }
  }
}
