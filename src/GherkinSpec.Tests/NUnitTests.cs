﻿using GherkinSpec.Core;
using GherkinSpec.Core.Loading;
using GherkinSpec.Core.Meta;
using GherkinSpec.XUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;


namespace GherkinSpec.Tests
{
  [TestFixture]  
  public class NUnitTests
  {


    [SetUp]
    public void Init()
    {
    }

    [TearDown]
    public void Cleanup()
    {
    }
    //protected override void Background()
    //{
    //  //Feature: @hicking
    //  var tags = Tags.ToList();
    //  //Assert.IsTrue(tags.Count == 1 && tags.All(d =>
    //  //  d == "hicking"));

      //  Step("Given a global administrator named 'Greg'");
      //  Step("And a blog named Greg's anti-tax rants");
      //  Step("And a customer named 'Wilson'");
      //  Step("And a blog named 'Expensive Therapy' owned by 'Wilson'");
      //} 

    [Test]
    public void NUnit_Buy_last_coffee()
    {
     // //Feature: @hicking
     // //Scenario: @billing @bicker @annoy  
     // var tags = Tags.ToList();
     // //Assert.IsTrue(tags.Count == 4 && tags.All(d =>
     // //  d == "hicking" || d == "billing" || d == "bicker" || d == "annoy"));

     // Step("Given a blog post named \"Random\" with:");

     //// var blogPostContent = ArgumentString;

     // Step("Given there are 1 coffees left in the machine");

     // Step("And I have deposited 1$");

     // Step("Given the following people exist:");

     // var people = ArgumentTable.Select(d => new Person { Name = d["name"] });

     // Step("When I press the coffee button");

     // Step("Then I should be served a coffee");

     // Step("Then the greeting service response will contain one of the following messages:");

     // var possibleResults = ResultList;
    }

    [Test]
    public void Buy_last_coffee_2()
    {
      //Step("Given there are 1 coffees left in the machine");

      //Step("And I have deposited 1$");

      //Step("Given the following people exist:");

      //var people = ArgumentTable.Select(d => new Person { Name = d["name"] });

      //Step("When I press the coffee button");

      //Step("Then I should be served a coffee");

      //Step("Then the greeting service response will contain one of the following messages:");

      //var possibleResults = ResultList;
    }

    public class EatData
    {
      public int start { get; set; }
      public int eat { get; set; }
      public int left { get; set; }
    }

    [Test]
    public void Eating()
    {
      //foreach (var exampleSet in ExampleSets)
      //  foreach (var example in exampleSet)
      //  {
      //    Step($"Given there are {example["start"]} cucumbers");

      //    Step($"When I eat {example["eat"]} cucumbers");

      //    Step($"Then I should have {example["left"]} cucumbers");
      //  }
    }

    [Test]
    public void All_scenarios_covered()
    {
       //AllScenariosCovered
    }
  }
}