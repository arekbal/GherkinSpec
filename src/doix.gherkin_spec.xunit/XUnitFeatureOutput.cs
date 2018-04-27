using System.Text;
using Xunit.Abstractions;
using doix.gherkin_spec.core;
using doix.gherkin_spec.core.Out;

namespace doix.gherkin_spec.xunit
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
