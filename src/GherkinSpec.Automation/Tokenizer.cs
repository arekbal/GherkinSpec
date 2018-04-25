using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GherkinSpec.Automation
{
  public enum TokenKind : byte
  {
    Start = (byte)'s',
    Finish = (byte)'f',
    Word = (byte)'w',
    Digit = (byte)'d',
    NewLine = (byte)'n',
    Error = (byte)'e',

    Space = (byte)' ',
    ParenthesisLeft = (byte)'(',
    ParenthesisRight = (byte)')',
    BracketLeft = (byte)'{',
    BracketRight = (byte)'}',
    Period = (byte)'.',
    Comma = (byte)',',
    Colon = (byte)':',
    SemiColon = (byte)';',
    SingleQuote = (byte)'\'',
    Slash = (byte)'/',
    BackSlash = (byte)'\\',
    Hyphen = (byte)'-',
    UnderScore = (byte)'_',
    Apostrophe = (byte)'\'',
    Exclamation = (byte)'!',
    QuestionMark = (byte)'?',
  }

  public class SyntaxToken
  {
    public TokenKind Kind { get; }
    public string Text { get; }

    public SyntaxToken(TokenKind kind, string text="")
    {
      Kind = kind;
      Text = text;
    }

    public override string ToString() => $"{{{Kind},\"{Text}\"}}";
  }

  class Tokenizer
  {
    readonly List<SyntaxToken> _tokens = new List<SyntaxToken>();

    readonly StringBuilder _buffer = new StringBuilder();

    char _currChar = '\0';

    TokenKind _currToken = TokenKind.Start;

    public IEnumerable<SyntaxToken> Tokens => _tokens;

    public void Tokenize(string statement)
    {
      _tokens.Clear();

      ResetToken();

      var c0 = (char)0;

      var methods = GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod).ToLookup(m => m.Name);

      for (var i = 0; i < statement.Length; i++)
      {
        _currChar = statement[i];

        var m = methods[$"On{_currToken}Token"].FirstOrDefault();

        if (m != null)
          m.Invoke(this, null);
        else
        {
          if (char.IsLetter(_currChar))
          {
            CloseToken();

            NewToken(TokenKind.Word);
          }
          else if (Enum.IsDefined(typeof(TokenKind), (byte)_currChar))
          {
            CloseToken();

            NewToken(_currChar);
          }
          else
            throw new NotSupportedException();
        }

        _buffer.Append(_currChar);

        if (_currChar == '\0')
          break;

        c0 = _currChar;
      }

      _tokens.Add(new SyntaxToken(_currToken, _buffer.ToString()));

      if (_tokens.Count == 1 || EndsWithError())
        _tokens.Add(new SyntaxToken(TokenKind.Finish));

      _buffer.Clear(); //done parsing... clear some memory up
    }

    void OnStartToken()
    {
      if (char.IsWhiteSpace(_currChar))
        return;

      if (char.IsLetter(_currChar))
      {
        CloseToken();

        NewToken(TokenKind.Word);
      }
    }

    void OnWordToken()
    {
      if (char.IsLetter(_currChar))
        return;

      if (IsCharToken(_currChar))
      {
        CloseToken();

        NewToken(_currChar);
      }
    }

    public override string ToString() => $"[{string.Join(", ", Tokens)}]";

    static bool IsCharToken(char c) => Enum.IsDefined(typeof(TokenKind), (byte)c);

    void NewToken(char c)
      => _currToken = (TokenKind)(byte)c;

    void NewToken(TokenKind token)
      => _currToken = token;

    void ResetToken()
      => _currToken = TokenKind.Start;

    bool EndsWithError()
      => _tokens[_tokens.Count - 1].Kind != TokenKind.Error;

    void CloseToken()
    {
      _tokens.Add(new SyntaxToken(_currToken, _buffer.ToString()));
      _buffer.Clear();
    }

    public string AsText() => string.Concat(_tokens.Select(t => t.Text));
  }
}
