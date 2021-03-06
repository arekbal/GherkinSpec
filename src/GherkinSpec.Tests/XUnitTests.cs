﻿using GherkinSpec.Core.Loading;
using GherkinSpec.XUnit;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace GherkinSpec.Tests
{
  [Feature(Loader = typeof(FeatureFileLoader), LoaderParam = "Feature.feature" /*FileAddress= "https://raw.githubusercontent.com/arekbal/arekbal.github.io/master/Feature.feature"*/)]
  public class XUnitTests : GherkinSpecBase
  {
    public XUnitTests(ITestOutputHelper outputHelper)
      :base(outputHelper)
    {
    }

    protected override void OnInitScenario()
    {
      base.OnInitScenario();
    }

    protected override void OnCleanupScenario(bool testPassed)
    {
      base.OnCleanupScenario(testPassed);
    }

    protected override void Background()
    {
      //Feature: @hicking
      var tags = Tags.ToList();
      //Assert.IsTrue(tags.Count == 1 && tags.All(d =>
      //  d == "hicking"));

      Step("Given a global administrator named 'Greg'");
      Step("And a blog named Greg's anti-tax rants");
      Step("And a customer named 'Wilson'");
      Step("And a blog named 'Expensive Therapy' owned by 'Wilson'");
    }   

    public class Person
    {
      public string Name { get; set; }
    }

    [Scenario]
    public void Buy_last_coffee()
    {
      //Feature: @hicking
      //Scenario: @billing @bicker @annoy  
      var tags = Tags.ToList();
      //Assert.IsTrue(tags.Count == 4 && tags.All(d =>
      //  d == "hicking" || d == "billing" || d == "bicker" || d == "annoy"));

      Step("Given a blog post named \"Random\" with:");

     // var blogPostContent = ArgumentString;

      Step("Given there are 1 coffees left in the machine");

      Step("And I have deposited 1$");

      Step("Given the following people exist:");

      var people = ArgumentTable.Select(d => new Person { Name = d["name"] });

      Step("When I press the coffee button");

      Step("Then I should be served a coffee");

      Step("Then the greeting service response will contain one of the following messages:");

      var possibleResults = ResultList;
    }

    [Scenario]
    public void Check_if_first_article_contains_G()
    {
      Given("I go to 'wp.pl'");

      Given("I open first link from 'text_topnews'");
    }

    [Scenario]
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

    [Scenario]
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

    [Fact]
    public void All_scenarios_covered()
    {
      Assert.True(AllScenariosCovered);      
    }
  }
}
