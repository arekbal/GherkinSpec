using Gherkin.Ast;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace doix.gherkin_spec.core
{
  using static Helpers;

  public class ExampleSets : IEnumerable<IEnumerable<IReadOnlyDictionary<string, string>>>
  {
    readonly IEnumerable<Examples> _examples;

    IEnumerable<IEnumerable<IReadOnlyDictionary<string, string>>> Cells =>
      _examples.Select(d => GetRowsValues(d.TableHeader.Cells.Select(cell => cell.Value), d.TableBody));

    public ExampleSets(IEnumerable<Examples> examples)
    {
      _examples = examples;
    }

    public IEnumerable<IReadOnlyDictionary<string, string>> this[string exampleName]
    {
      get
      {
        var example = _examples.FirstOrDefault(d => d.Name == exampleName);

        var columnNames = example.TableHeader.Cells.Select(d => d.Value);

        return GetRowsValues(columnNames, example.TableBody);
      }
    }

    public IEnumerable<IReadOnlyDictionary<string, string>> this[int index]
    {
      get
      {
        var example = _examples.ElementAt(index);

        var columnNames = example.TableHeader.Cells.Select(d => d.Value);

        return GetRowsValues(columnNames, example.TableBody);
      }
    }

    public IEnumerator<IEnumerable<IReadOnlyDictionary<string, string>>> GetEnumerator() => Cells.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => Cells.GetEnumerator();
  }
}
