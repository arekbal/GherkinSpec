using Gherkin.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.String;

namespace GherkinSpec.Core
{
  using FeatureLoading;
  using FeatureWriting;
  using static Helpers;

  public class GherkinSpecContext
  {
    public object TestContainer { get; private set; }

    public Feature CurrFeature { get; private set; }

    public ScenarioDefinition CurrScenario { get; private set; }

    public Step CurrStep { get; private set; }

    int _backgroundStep;

    int _scenarioStep;

    bool _featureInitialized;

    IFeatureWriter _writer = null;

    public GherkinSpecContext()
    {  
    }

    public void InitFeature(object testContainer)
    {
      if (!_featureInitialized)
      {
        _writer = CreateWriter();

        TestContainer = testContainer;

        var attr = (FeatureAttribute)TestContainer.GetType().GetCustomAttributes(typeof(FeatureAttribute), true).FirstOrDefault();

        if (attr == null)
          throw FeatureError("Feature attribute is missing");

        var bFeature = FeatureLoaderSelector.Select(attr);
        if (bFeature.NoValue)
          throw FeatureError("Coudn't load Feature");

        CurrFeature = bFeature.Value;
      }

        _writer.WriteLine(CurrFeature.Keyword.Trim(), CurrFeature.Name, this);

        if (!IsNullOrWhiteSpace(CurrFeature.Description))
          _writer.WriteLine(CurrFeature.Description, this);

        _writer.WriteLine("", this);

        if (CurrFeature.Background != null)
        {
          _writer.WriteLine(CurrFeature.Background.Keyword.Trim(), CurrFeature.Background.Name, this);

          if (!IsNullOrWhiteSpace(CurrFeature.Background.Description))
            _writer.WriteLine(CurrFeature.Background.Description, this);
        }

        _backgroundStep = 0;

        _featureInitialized = true;      
    }

    public ExampleSets ExampleSets
    {
      get
      {
        if(CurrScenario is ScenarioOutline)
          return new ExampleSets(((ScenarioOutline)CurrScenario).Examples);

        throw FeatureError("Current scenario isn't an outline");
      }
    }

    public void InitScenario(string methodName)
    {
      CurrScenario = null;

      CurrStep = null;
      
      _scenarioStep = 0;

      var attr = (ScenarioAttribute)TestContainer
        .GetType()
        .GetMethod(methodName)
        .GetCustomAttributes(typeof(ScenarioAttribute), false)
        .FirstOrDefault();

      if (attr == null)
        return;

      CurrScenario = CurrFeature.ScenarioDefinitions.FirstOrDefault(d => d.Name == attr.ScenarioName);

      if (CurrScenario == null)
        throw FeatureError($"There is no scenario under name: '{attr.ScenarioName}' defined in .feature file");

      _writer.WriteLine("", this);
      _writer.WriteLine(CurrScenario.Keyword.Trim(), CurrScenario.Name, this);

      if(!IsNullOrWhiteSpace(CurrScenario.Description))
        _writer.WriteLine(CurrScenario.Description, this);
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
      _writer.WriteLine("", this);
      
      if (testPassed)
      {
        if (CurrFeature.Background.Steps.Count() != _backgroundStep)
        {
          ResetScenario();
          throw FeatureError("Not enough or too many steps");
        }

        if (CurrScenario is ScenarioOutline)
        {
          var outline = (ScenarioOutline)CurrScenario;

          WithExamples();

          if (outline.Steps.Count() * outline.Examples.Sum(e => e.TableBody.Count()) != _scenarioStep)
          {
            ResetScenario();
            throw FeatureError("Not enough or too many steps");
          }
          else
          {
            ResetScenario();
            return;
          }
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
          .Select(method => (ScenarioAttribute)method.GetCustomAttributes(typeof(ScenarioAttribute), false).FirstOrDefault())
          .Where(IsNotNull)
          .Select(d => d.ScenarioName);

        return CurrFeature.ScenarioDefinitions.All(d=> coveredScenarioNames.Contains(d.Name));
      }
    }

    protected virtual IFeatureWriter CreateWriter() => new FeatureConsoleWriter();

    Gherkin.Ast.Step GetCurrStep()
    {
      if (CurrScenario == null)
        return CurrFeature.Background.Steps.ElementAt(_backgroundStep);

      if (CurrScenario is ScenarioOutline)
      {
        var outline = (ScenarioOutline)CurrScenario;
        var exampleSetsCount = outline.Examples.Sum(d => d.TableBody.Count());

        return CurrScenario.Steps.ElementAt(_scenarioStep % CurrScenario.Steps.Count());
      }

      return CurrScenario.Steps.ElementAt(_scenarioStep);     
    }

    void InternalStep(string textStartingWithKeywordAndPlaceholders)
    {
      var fullText = $"{CurrStep.Keyword.Trim()} {CurrStep.Text}";

      if (CurrScenario is ScenarioOutline)
      {
        var outline = (ScenarioOutline)CurrScenario;

        var exampleRows = outline.Examples.SelectMany(ex => ex.TableBody);

        var stepModelRow = exampleRows.ElementAt(_scenarioStep / CurrScenario.Steps.Count());

        var columnNames = outline.Examples.FirstOrDefault().TableHeader.Cells.Select(c => c.Value);

        var dict = GetRowAsDict(columnNames, stepModelRow);

        foreach (var columnName in columnNames)
          fullText = fullText.Replace($"<{columnName}>", dict[columnName]);
      }

      if (fullText.Trim() != textStartingWithKeywordAndPlaceholders)
        throw FeatureError($"Expected different text... expected: '{fullText}', got:'{textStartingWithKeywordAndPlaceholders}'");

      if (CurrScenario is ScenarioOutline)
        _writer.WriteLine(fullText, this);
      else
        _writer.WriteLine(CurrStep.Keyword.Trim(), CurrStep.Text, this);

      if (CurrStep.Argument != null)
        if (IsArgumentList)
          WithArgumentList();
        else
          WithArgumentTable();

      IncrementStep();
    }

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
        throw FeatureError($"Expected different keyword... expected: '{CurrStep.Keyword}', got:'{keyword}'");

      InternalStep($"{keyword} {textWithPlaceholders}");
    }

    void IncrementStep()
    {
      if (CurrScenario != null)
        _scenarioStep++;
      else
        _backgroundStep++;
    }

    bool IsArgumentTable => ((DataTable)CurrStep.Argument).Rows.First().Cells.Take(2).Count() > 1;

    bool IsArgumentList => ((DataTable)CurrStep.Argument).Rows.First().Cells.Take(2).Count() == 1;

    void WithArgumentTable()
    {
      _writer.WriteLine($"| {Join(" | ", ArgumentTable.First().Select(d => d.Key))} |", this);

      foreach (var args in ArgumentTable)
        _writer.WriteLine($"| {Join(" | ", args.Select(d => d.Value))} |", this);
    }

    void WithArgumentList()
    {
      foreach (var arg in ArgumentList)
        _writer.WriteLine($"| {arg} |", this);
    }

    void WithExamples()
    {
      var outline = (ScenarioOutline)CurrScenario;

      foreach (var examplesObject in outline.Examples)
      {
        _writer.WriteLine(examplesObject.Keyword.Trim(), examplesObject.Name, this);
        _writer.WriteLine($"| {Join(" | ", examplesObject.TableHeader.Cells.Select(d => d.Value))} |", this);

        foreach (var exampleRow in examplesObject.TableBody)
          _writer.WriteLine($"| {Join(" | ", exampleRow.Cells.Select(d => d.Value))} |", this);
      }
    }

    public IEnumerable<string> ArgumentList => ((DataTable)CurrStep.Argument).Rows.Select(d=>d.Cells.First().Value);

    public IEnumerable<IReadOnlyDictionary<string, string>> ArgumentTable => GetRowsValues(((DataTable)CurrStep.Argument).Rows);

    protected static Exception FeatureError(string message) => new Exception($"[FeatureError] {message}");
  }
}
