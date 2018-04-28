using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinSpec.Core.Out
{
  public interface IFeatureOutput : IDisposable
  {
    void Write(string keyword, string text, GherkinSpecContext specContext);
    void WriteLine(string keyword, string text, GherkinSpecContext specContext);
    void Write(string text, GherkinSpecContext specContext);
    void WriteLine(string text, GherkinSpecContext specContext);
  }
}
