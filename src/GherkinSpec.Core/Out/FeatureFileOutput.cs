using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinSpec.Core.Out
{
  public class FeatureFileOutput : IFeatureOutput
  {
    public string FilePath { get; set; }

    public void Write(string keyword, string text, GherkinSpecContext specContext) => File.AppendAllText(FilePath, $"{keyword}: {text}");

    public void Write(string text, GherkinSpecContext specContext) => File.AppendAllText(FilePath, text);

    public void WriteLine(string keyword, string text, GherkinSpecContext specContext) => File.AppendAllText(FilePath, $"{keyword}: {text}{Environment.NewLine}");

    public void WriteLine(string text, GherkinSpecContext specContext) => File.AppendAllText(FilePath, text + Environment.NewLine);
  }
}
