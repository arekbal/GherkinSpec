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

    readonly StringBuilder _buffer = new StringBuilder();

    public XUnitFeatureOutput(ITestOutputHelper outputHelper)
    {
      _outputHelper = outputHelper;
    }

    public void Write(string text, GherkinSpecContext specContext)
    {
      _buffer.Append(text);
    }

    public void Write(string keyword, string text, GherkinSpecContext specContext)
    {
      _buffer.Append(keyword);
      _buffer.Append(text);
    }

    public void WriteLine(string text, GherkinSpecContext specContext)
    {
      _buffer.Append(text);

      _outputHelper.WriteLine(_buffer.ToString());

      _buffer.Clear();
    }

    public void WriteLine(string keyword, string text, GherkinSpecContext specContext)
    {
      _buffer.Append(keyword);
      _buffer.Append(text);

      _outputHelper.WriteLine(_buffer.ToString());

      _buffer.Clear();
    }

    public void Dispose()
    {
      if (_buffer.Length > 0)
      {
        _outputHelper.WriteLine(_buffer.ToString());
        _buffer.Clear();
      }
    }
  }
}
