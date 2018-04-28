using Gherkin.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.String;

namespace GherkinSpec.Core
{
  using Meta;
  using Loading;
  using Out;
  using static Helpers;

  public class GherkinSpecContext
  {
    public object TestContainer { get; private set; }

    public string TestDirectory { get; private set; }

    public Feature CurrFeature { get; private set; }

    public ScenarioDefinition CurrScenario { get; private set; }

    public Step CurrStep { get; private set; }

    int _backgroundStep;

    int _scenarioStep;

    bool _featureInitialized;

    IFeatureOutput _output;
  
    public void InitFeature(object testContainer, string testDirectory=null)
    { 
      if (!_featureInitialized)
      {
        _output = CreateOutput();

        TestContainer = testContainer;

        TestDirectory = testDirectory ?? Environment.CurrentDirectory;

        var attr = TestContainer
          .GetType()
          .GetCustomAttributes(typeof(IFeatureAttribute), true)
          .Cast<IFeatureAttribute>()
          .FirstOrDefault();

        if (attr == null)
          throw FeatureError("Feature attribute is missing");

        var bFeature = FeatureLoaderSelector.Select(attr, TestContainer, TestDirectory);
        if (bFeature.NoValue)
          throw FeatureError("Coudn't load Feature");

        CurrFeature = bFeature.Value;
      }

      _output.WriteLine(CurrFeature.Keyword.Trim(), ": " + CurrFeature.Name, this);

      if (!IsNullOrWhiteSpace(CurrFeature.Description))
        _output.WriteLine(CurrFeature.Description, this);

      _output.WriteLine("", this);

      var background = CurrFeature.Children.OfType<Background>().FirstOrDefault();
      if (background != null)
      {
        _output.WriteLine(background.Keyword.Trim(), ": " + background.Name, this);

        if (!IsNullOrWhiteSpace(background.Description))
          _output.WriteLine(background.Description, this);
      }

      _backgroundStep = 0;

      _featureInitialized = true;   
    }

    public ExampleSets ExampleSets
    {
      get
      {
        if(CurrScenario is ScenarioOutline outline)
          return new ExampleSets(outline.Examples);

        throw FeatureError("Current scenario isn't an outline");
      }
    }

    public void InitScenario(string methodName)
    {
      CurrScenario = null;

      CurrStep = null;
      
      _scenarioStep = 0;

      var attr = TestContainer
        .GetType()
        .GetMethod(methodName)
        .GetCustomAttributes(typeof(IScenarioAttribute), true)
        .Cast<IScenarioAttribute>()
        .FirstOrDefault();

      if (attr == null)
        return;

      var scenarioName = attr.ScenarioName ?? methodName.Replace('_', ' ');

      CurrScenario = CurrFeature.Children.FirstOrDefault(d => d.Name == scenarioName);

      if (CurrScenario == null)
        throw FeatureError($"There is no scenario under name: '{scenarioName}' defined in .feature file");

      _output.WriteLine("", this);
      _output.WriteLine(CurrScenario.Keyword.Trim(), ": " + CurrScenario.Name, this);

      if(!IsNullOrWhiteSpace(CurrScenario.Description))
        _output.WriteLine(CurrScenario.Description, this);
    }

    void ResetScenario()
    {
      CurrScenario = null;
      CurrStep = null;
      _backgroundStep = 0;
      _scenarioStep = 0;
    }

    public void CleanupScenario(bool testPassed)
    {
      _output.WriteLine("", this);
      
      if (testPassed)
      {
        var background = CurrFeature.GetBackgroundOrNull();
        if (background != null && background.Steps.Count() != _backgroundStep)
        {
          ResetScenario();
          throw FeatureError("Not enough or too many steps");
        }

        if (CurrScenario is ScenarioOutline outline)
        {
          WithExamples();

          if (outline.Steps.Count() * outline.Examples.Sum(e => e.TableBody.Count()) != _scenarioStep)
          {
            ResetScenario();
            throw FeatureError("Not enough or too many steps");
          }
         
          ResetScenario();
          return;
        }

        if (CurrScenario != null && CurrScenario.Steps.Count() != _scenarioStep)
        {
          ResetScenario();
          throw FeatureError("Not enough or too many steps");
        }
      }

      ResetScenario();
    }

    public void CleanupFeature()
    {
      _backgroundStep = 0;
    }

    public bool AllScenariosCovered
    {
      get
      {
        var coveredScenarioNames = 
          TestContainer
          .GetType()
          .GetMethods()
          .Select(method => new { Method = method, Attrib = (IScenarioAttribute)method.GetCustomAttributes(typeof(IScenarioAttribute), true).FirstOrDefault() })
          .Where(d=>IsNotNull(d.Attrib))
          .Select(d => d.Attrib.ScenarioName ?? d.Method.Name.Replace('_', ' '));

        return CurrFeature.Children.Where(x=> !(x is Background)).All(d=> coveredScenarioNames.Contains(d.Name));
      }
    }

    protected virtual IFeatureOutput CreateOutput() => new FeatureConsoleOutput();

    Step GetCurrStep()
    {
      if (CurrScenario == null)
      {
        var background = CurrFeature.GetBackgroundOrNull();

        if (background != null)
          return background.Steps.ElementAt(_backgroundStep);

        return null;
      }

      if (CurrScenario is ScenarioOutline outline)
      {
        var exampleSetsCount = outline.Examples.Sum(d => d.TableBody.Count());

        return CurrScenario.Steps.ElementAt(_scenarioStep % CurrScenario.Steps.Count());
      }

      return CurrScenario.Steps.ElementAt(_scenarioStep);     
    }

    public IEnumerable<string> Tags => 
      CurrScenario != null ?
        CurrFeature.Tags.Concat(CurrScenario.GetTags()).Select(t => t.Name.Substring(1)) :
        CurrFeature.Tags.Select(t => t.Name.Substring(1));

    public void Step(string textStartingWithKeywordAndPlaceholders)
    {
      CurrStep = GetCurrStep();

      InternalStep(textStartingWithKeywordAndPlaceholders);
    }

    public void Step(string keyword, string textWithPlaceholders)
    {
      CurrStep = GetCurrStep();

      if (CurrStep == null)
        throw FeatureError("That step is not defined in specification");

      if (CurrStep.Keyword.Trim() != keyword)
        throw FeatureError($"Expected different keyword... expected: '{CurrStep.Keyword.Trim()}', got:'{keyword}'");

      InternalStep(keyword, textWithPlaceholders);
    }

    void InternalStep(string keyword, string textWithPlaceholders)
    {
      var textStartingWithKeywordAndPlaceholders = $"{keyword} {textWithPlaceholders}";

      ValidateScenario(textStartingWithKeywordAndPlaceholders);

      if (CurrScenario is ScenarioOutline)
        _output.WriteLine(keyword, textWithPlaceholders, this);
      else
        _output.WriteLine(CurrStep.Keyword.Trim(), " " + CurrStep.Text, this);

      PrintArguments();

      IncrementStep();
    }
    void InternalStep(string textStartingWithKeywordAndPlaceholders)
    {
      ValidateScenario(textStartingWithKeywordAndPlaceholders);

      if (CurrScenario is ScenarioOutline)
        _output.WriteLine(textStartingWithKeywordAndPlaceholders, this);
      else
        _output.WriteLine(CurrStep.Keyword.Trim(), " " + CurrStep.Text, this);

      PrintArguments();

      IncrementStep();
    }

    private void PrintArguments()
    {
      if (CurrStep.Argument != null)
        if (IsArgumentString)
          WithArgumentString();
        else if (IsArgumentList)
          WithArgumentList();
        else
          WithArgumentTable();
    }

    private void ValidateScenario(string textStartingWithKeywordAndPlaceholders)
    {
      var fullText = $"{CurrStep.Keyword.Trim()} {CurrStep.Text}";

      if (CurrScenario is ScenarioOutline outline)
      {
        var exampleRows = outline.Examples.SelectMany(ex => ex.TableBody);

        var stepModelRow = exampleRows.ElementAt(_scenarioStep / CurrScenario.Steps.Count());

        var columnNames = outline.Examples.FirstOrDefault().TableHeader.Cells.Select(c => c.Value);

        var dict = GetRowAsDict(columnNames, stepModelRow);

        foreach (var columnName in columnNames)
          fullText = fullText.Replace($"<{columnName}>", dict[columnName]);
      }

      if (fullText.Trim() != textStartingWithKeywordAndPlaceholders)
        throw FeatureError($"Expected different text... expected: '{fullText}', got:'{textStartingWithKeywordAndPlaceholders}'");
    }

    void IncrementStep()
    {
      if (CurrScenario != null)
        _scenarioStep++;
      else
        _backgroundStep++;
    }

    public bool IsArgumentString => CurrStep.Argument is DocString;

    public bool IsArgumentTable => !IsArgumentString && ((DataTable)CurrStep.Argument).Rows.First().Cells.Take(2).Count() > 1;

    public bool IsArgumentList => !IsArgumentString && ((DataTable)CurrStep.Argument).Rows.First().Cells.Take(2).Count() == 1;


    void WithArgumentString()
    {
      var nl = Environment.NewLine;
      _output.WriteLine($"\"\"\"{nl}{ArgumentString}{nl}\"\"\"", this);
    }

    void WithArgumentTable()
    {
      _output.WriteLine($"| {Join(" | ", ArgumentTable.First().Select(d => d.Key))} |", this);

      foreach (var args in ArgumentTable)
        _output.WriteLine($"| {Join(" | ", args.Select(d => d.Value))} |", this);
    }

    void WithArgumentList()
    {
      foreach (var arg in ArgumentList)
        _output.WriteLine($"| {arg} |", this);
    }

    void WithExamples()
    {
      var outline = (ScenarioOutline)CurrScenario;

      foreach (var examplesObject in outline.Examples)
      {
        _output.WriteLine(examplesObject.Keyword.Trim(), ": " + examplesObject.Name, this);
        _output.WriteLine($"| {Join(" | ", examplesObject.TableHeader.Cells.Select(d => d.Value))} |", this);

        foreach (var exampleRow in examplesObject.TableBody)
          _output.WriteLine($"| {Join(" | ", exampleRow.Cells.Select(d => d.Value))} |", this);
      }
    }

    public string ArgumentString => ((DocString)CurrStep.Argument).Content;

    public IEnumerable<string> ArgumentList => ((DataTable)CurrStep.Argument).Rows.Select(d=>d.Cells.First().Value);

    public IEnumerable<IReadOnlyDictionary<string, string>> ArgumentTable => GetRowsValues(((DataTable)CurrStep.Argument).Rows);

    protected static Exception FeatureError(string message) => new Exception($"[FeatureError] {message}");
  }
}
