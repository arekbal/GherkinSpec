using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinSpec.Core.FeatureWriting
{
  public interface IFeatureWriter
  {
    void Write(string keyword, string text, GherkinSpecContext specContext);
    void WriteLine(string keyword, string text, GherkinSpecContext specContext);
    void Write(string text, GherkinSpecContext specContext);
    void WriteLine(string text, GherkinSpecContext specContext);
  }
}
