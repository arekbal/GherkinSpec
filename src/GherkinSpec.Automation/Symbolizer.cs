using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GherkinSpec.Automation
{
  public enum SymbolKind
  {
    Start,
    Word,
    Number,
    Period,
    Comma,
    Semicolon,
    QuotedText,    
    End
  }

  interface ISyntaxSymbol
  {
    SymbolKind Kind { get; }

    string Text { get; }

    string ToString();
  }

  class SyntaxSymbol : ISyntaxSymbol
  {
    public SymbolKind Kind { get; }

    public string Text { get; }

    public SyntaxSymbol(SymbolKind kind, string text="")
    {
      Kind = kind;
      Text = text;
    }

    public override string ToString() => $"{{{Kind},\"{Text}\"}}";
  }

  class Symbolizer
  {
    readonly List<ISyntaxSymbol> _symbols = new List<ISyntaxSymbol>();

    public IEnumerable<ISyntaxSymbol> Symbols => _symbols;

    readonly List<SyntaxToken> _buffer = new List<SyntaxToken>();

    SyntaxSymbol _currSymbol;

    SyntaxToken _currToken;

    public void Symbolize(IEnumerable<SyntaxToken> tokens)
    {
      _symbols.Clear();

      ResetSymbol();

      var methods = GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod).ToLookup(m => m.Name);
     
      foreach (var token in tokens)
      {
        var m = methods[$"On{_currSymbol}Symbol"].FirstOrDefault();

        if (m == null)
          throw new NotSupportedException();

        m.Invoke(this, null);         
      }

      _buffer.Clear();
    }

    void OnStartSymbol()
    {
      //if(_currToken.Kind != TokenKind.)

      //CloseSymbol();
    }

    void CloseSymbol(SyntaxSymbol newSymbol)
    {
      _symbols.Add(_currSymbol);
      _currSymbol = newSymbol;
    }

    void ResetSymbol() => _currSymbol = new SyntaxSymbol(SymbolKind.Start);

    public override string ToString() => $"[{string.Join(", ", Symbols)}]";

    public string AsText() => string.Concat(_symbols.Select(t => t.Text));
  }
}
