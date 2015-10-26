using Gherkin.Ast;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.String;

namespace GherkinSpec
{
  public class GherkinSpecContext
  {
    public Type TestContainer { get; }

    public Feature Feature { get; }

    ScenarioDefinition CurrScenario { get; set; }

    Step CurrStep { get; set; }

    int step = 0;

    public GherkinSpecContext(Type testContainer)
    {
      TestContainer = testContainer;

      var attr = (FeatureAttribute)TestContainer.GetCustomAttributes(typeof(FeatureAttribute), false).FirstOrDefault();

      if (attr == null)
        throw FeatureError("Feature attribute is missing");

      var parser = new Gherkin.Parser();

      if (!IsNullOrWhiteSpace(attr.FilePath))
        Feature = parser.Parse(attr.FilePath);
      else
        if (!IsNullOrWhiteSpace(attr.FileAddress))
      {
        using (var httpClient = new HttpClient())
        using (var reader = new StreamReader(httpClient.GetStreamAsync(attr.FileAddress).Result))
          Feature = parser.Parse(reader);
      }
      else
        throw FeatureError("No FilePath or FileAddress");
    }

    public void InitFeature()
    {
      WriteLine(Feature.Keyword + ": " + Feature.Name);
      WriteLine(Feature.Description);
      WriteLine("");
    }

    public void InitScenario(string methodName)
    {
      CurrScenario = null;

      CurrStep = null;

      step = 0;

      var attr = (ScenarioAttribute)TestContainer.GetMethod(methodName).GetCustomAttributes(typeof(ScenarioAttribute), false).FirstOrDefault();

      if (attr == null)
        return;

      CurrScenario = Feature.ScenarioDefinitions.FirstOrDefault(d => d.Name == attr.ScenarioName);

      if (CurrScenario == null)
        throw FeatureError($"There is no scenario under name: '{attr.ScenarioName}' defined in .feature file");

      WriteLine(CurrScenario.Keyword + ": " + CurrScenario.Name);
      WriteLine(CurrScenario.Description);
    }

    public void CleanupScenario()
    {
      if (Feature.Background.Steps.Concat(CurrScenario.Steps).Count() != step)
        throw FeatureError("Not enough steps");

      WriteLine("");
    }

    protected virtual void WriteLine(string text, ConsoleColor foreColor = ConsoleColor.DarkCyan, ConsoleColor backColor = ConsoleColor.Black)
    {
      Console.ForegroundColor = foreColor;
      Console.BackgroundColor = backColor;

      Console.WriteLine(text);
      //TestContext.WriteLine(text);
      //Debug.WriteLine(text);

      Console.ResetColor();
    }

    public void Step(string textStartingWithKeyword)
    {
      CurrStep = Feature.Background.Steps.Concat(CurrScenario.Steps).ElementAt(step);

      var fullText = CurrStep.Keyword.Trim() + " " + CurrStep.Text;

      if (fullText.Trim() != textStartingWithKeyword)
        throw FeatureError($"Expected different text... expected: '{fullText}', got:'{textStartingWithKeyword}'");

      WriteLine(fullText);

      if (CurrStep.Argument != null)
        if (IsArgumentList)
          WithArgumentList();
        else
          WithArgumentTable();

      step++;
    }

    public void Step(string keyword, string text)
    {
      CurrStep = Feature.Background.Steps.Concat(CurrScenario.Steps).ElementAt(step);

      if (CurrStep == null)
        throw FeatureError("That step is not defined by specification");

      if (CurrStep.Keyword.Trim() != keyword)
        throw FeatureError($"Expected different keyword... expected: '{CurrStep.Keyword}', got:'{keyword}'");

      if (CurrStep.Text.Trim() != text.Trim())
        throw FeatureError($"Expected different text... expected: '{CurrStep.Text}', got:'{text}'");

      WriteLine(CurrStep.Keyword.Trim() + " " + CurrStep.Text);

      step++;
    }

    private bool IsArgumentTable => ((DataTable)CurrStep.Argument).Rows.First().Cells.Take(2).Count() > 1;
    private bool IsArgumentList => ((DataTable)CurrStep.Argument).Rows.First().Cells.Take(2).Count() == 1;

    private void WithArgumentTable()
    {
      WriteLine("| " + Join(" | ", ArgumentTable.First().Select(d => d.Key)) + " |");

      foreach (var args in ArgumentTable)
        WriteLine("| " + Join(" | ", args.Select(d => d.Value)) + " |");
    }

    private void WithArgumentList()
    {
      foreach (var arg in ArgumentList)
        WriteLine("| " + arg + " |");
    }

    public IEnumerable<string> ArgumentList => ((DataTable)CurrStep.Argument).Rows.Select(d=>d.Cells.First().Value);

    public IEnumerable<IReadOnlyDictionary<string, string>> ArgumentTable => GetRowValues(((DataTable)CurrStep.Argument).Rows);

    protected static Exception FeatureError(string message) => new Exception("[FeatureError] " + message);

    static IEnumerable<IReadOnlyDictionary<string, string>> GetRowValues(IEnumerable<TableRow> tableRows)
    {
      var columnNames = tableRows.First().Cells.Select(c => c.Value);

      foreach (var row in tableRows.Skip(1))
      {
        var dict = new Dictionary<string, string>();

        using (var enumerator = row.Cells.GetEnumerator())
          foreach (var columnName in columnNames)
          {
            enumerator.MoveNext();
            dict.Add(columnName, enumerator.Current.Value);
          }

        yield return dict;
      }
    }
  }
}
