using Gherkin.Ast;
using System.Collections.Generic;
using System.Linq;

namespace GherkinSpec.Core
{
  public static class Helpers
  {
    public static IEnumerable<IReadOnlyDictionary<string, string>> GetRowsValues(IEnumerable<TableRow> tableRows)
    {
      var columnNames = tableRows.First().Cells.Select(c => c.Value);

      foreach (var row in tableRows.Skip(1))
        yield return GetRowAsDict(columnNames, row);
    }

    public static IEnumerable<IReadOnlyDictionary<string, string>> GetRowsValues(IEnumerable<string> columnNames, IEnumerable<TableRow> rows)
    {
      foreach (var row in rows)
        yield return GetRowAsDict(columnNames, row);
    }

    public static IReadOnlyDictionary<string, string> GetRowAsDict(IEnumerable<string> columnNames, TableRow row)
    {
      var dict = new Dictionary<string, string>();

      using (var enumerator = row.Cells.GetEnumerator())
        foreach (var columnName in columnNames)
        {
          enumerator.MoveNext();
          dict.Add(columnName, enumerator.Current.Value);
        }

      return dict;
    }

    public static bool IsNotNull<T>(T item) where T : class => item != null;
  }
}
