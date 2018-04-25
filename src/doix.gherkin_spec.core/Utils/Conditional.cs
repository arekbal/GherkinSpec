using System;

namespace doix.gherkin_spec.core.Utils
{
  public static class Conditional
  {
    public static Conditional<T> From<T>(T value) where T : class
    {
      return new Conditional<T>(value);
    }   
  }

  public class Conditional<T> where T : class
  {
    readonly T _value;

    readonly bool _hasValue;

    public Conditional(T value)
    {
      if (value == null)
        throw new Exception("value is null");

      _value = value;

      _hasValue = true;
    }

    private Conditional()
    {
    }

    public bool HasValue => _hasValue;

    public bool NoValue => !_hasValue;

    public T Value
    {
      get
      {
        if(_hasValue)
          return _value;

        throw new Exception("no value");
      }
    }

    public override string ToString()
    {
      if(HasValue)
        return Value.ToString();

      return "{Conditional: No Value}";
    }

    public static readonly Conditional<T> Empty = new Conditional<T>();
  }
}
