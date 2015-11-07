using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GherkinSpec.Core.FeatureLoading
{
  public class FeatureHttpLoader : IFeatureLoader
  {
    public TextReader ReadFeatureContent(string address)
    {
      using (var webClient = new WebClient())
        return new StreamReader(webClient.OpenRead(address));
    }
  }
}
