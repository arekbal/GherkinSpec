using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinSpec.Core.FeatureWriting
{
  public class FeatureConsoleWriter : IFeatureWriter
  {
    public ConsoleColor? KeywordColor { get; set; }
    public ConsoleColor? TextColor { get; set; }
    public ConsoleColor? BackColor { get; set; }

    public virtual void Write(string keyword, string text, GherkinSpecContext specContext)
    {
      if (KeywordColor.HasValue)
        Console.ForegroundColor = KeywordColor.Value;

      if (BackColor.HasValue)
        Console.BackgroundColor = BackColor.Value;

      Console.Write(keyword);
      Console.Write(": ");

      Console.ResetColor();

      if (TextColor.HasValue)
        Console.ForegroundColor = TextColor.Value;

      if (BackColor.HasValue)
        Console.BackgroundColor = BackColor.Value;

      Console.Write(text);

      Console.ResetColor();
    }

    public virtual void WriteLine(string keyword, string text, GherkinSpecContext specContext)
    {
      if (KeywordColor.HasValue)
        Console.ForegroundColor = KeywordColor.Value;

      if (BackColor.HasValue)
        Console.BackgroundColor = BackColor.Value;

      Console.Write(keyword);
      Console.Write(": ");

      Console.ResetColor();

      if (TextColor.HasValue)
        Console.ForegroundColor = TextColor.Value;

      if (BackColor.HasValue)
        Console.BackgroundColor = BackColor.Value;

      Console.WriteLine(text);

      Console.ResetColor();
    }

    public virtual void Write(string text, GherkinSpecContext specContext)
    {
      if (TextColor.HasValue)
        Console.ForegroundColor = TextColor.Value;

      if (BackColor.HasValue)
        Console.BackgroundColor = BackColor.Value;

      Console.Write(text);

      Console.ResetColor();
    }

    public virtual void WriteLine(string text, GherkinSpecContext specContext)
    {
      if (TextColor.HasValue)
        Console.ForegroundColor = TextColor.Value;

      if (BackColor.HasValue)
        Console.BackgroundColor = BackColor.Value;

      Console.WriteLine(text);

      Console.ResetColor();
    }
  }
}
