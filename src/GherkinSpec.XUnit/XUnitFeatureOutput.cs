using GherkinSpec.Core.Out;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GherkinSpec.Core;
using Xunit.Abstractions;

namespace GherkinSpec.XUnit
{
  class XUnitFeatureOutput : IFeatureOutput
  {
    readonly ITestOutputHelper _outputHelper;

    string _buffer;

    public XUnitFeatureOutput(ITestOutputHelper outputHelper)
    {
      _outputHelper = outputHelper;
    }

    public void Write(string text, GherkinSpecContext specContext)
    {
      _buffer += text;
    }

    public void Write(string keyword, string text, GherkinSpecContext specContext)
    {
      _buffer += keyword + ": " + text;
    }

    public void WriteLine(string text, GherkinSpecContext specContext)
    {
      _buffer += text;

      _outputHelper.WriteLine(_buffer);

      _buffer = "";
    }

    public void WriteLine(string keyword, string text, GherkinSpecContext specContext)
    {
      _buffer += keyword + ": " + text;

      _outputHelper.WriteLine(_buffer);

      _buffer = "";
    }

    public void Dispose()
    {
      if (!String.IsNullOrEmpty(_buffer))
        _outputHelper.WriteLine(_buffer);

      _buffer = "";
    }
  }
}
