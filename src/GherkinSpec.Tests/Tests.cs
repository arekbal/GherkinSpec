
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GherkinSpec.Core;
using GherkinSpec.MsTest;
using System.Linq;
using GherkinSpec.Core.Loading;

namespace GherkinSpec.Tests
{
  [Feature(Loader=typeof(FeatureFileLoader), LoaderParam="Feature.feature" /*FileAddress= "https://raw.githubusercontent.com/arekbal/arekbal.github.io/master/Feature.feature"*/) TestClass]
  public class Tests : GherkinSpecBase
  {
    protected override void Background()
    {
      Step("Given a global administrator named 'Greg'");
      Step("And a blog named Greg's anti-tax rants");
      Step("And a customer named 'Wilson'");
      Step("And a blog named 'Expensive Therapy' owned by 'Wilson'");
    }

    public class Person
    {
      public string Name { get; set; }
    }

    [Scenario TestMethod]
    public void Buy_last_coffee()
    {
      Step("Given there are 1 coffees left in the machine");

      Step("And I have deposited 1$");

      Step("Given the following people exist:");

      var people = ArgumentTable.Select(d => new Person { Name = d["name"] });

      Step("When I press the coffee button");
      Step("Then I should be served a coffee");
      Step("Then the greeting service response will contain one of the following messages:");

      var possibleResults = ResultList;
    }

    [Scenario TestMethod]
    public void Buy_last_coffee_2()
    {
      Step("Given there are 1 coffees left in the machine");

      Step("And I have deposited 1$");

      Step("Given the following people exist:");

      var people = ArgumentTable.Select(d => new Person { Name = d["name"] });

      Step("When I press the coffee button");

      Step("Then I should be served a coffee");

      Step("Then the greeting service response will contain one of the following messages:");

      var possibleResults = ResultList;
    }

    public class EatData
    {
      public int start { get; set; }
      public int eat { get; set; }
      public int left { get; set; }
    }        
 
    
    [Scenario TestMethod]
    public void Eating()
    {
      foreach (var exampleSet in ExampleSets)
        foreach (var example in exampleSet)
        {
          Step($"Given there are {example["start"]} cucumbers");

          Step($"When I eat {example["eat"]} cucumbers");

          Step($"Then I should have {example["left"]} cucumbers");
        }
    }

    [TestMethod]
    public void All_scenarios_covered()
    {
      Assert.IsTrue(AllScenariosCovered);
    }
  }
}
