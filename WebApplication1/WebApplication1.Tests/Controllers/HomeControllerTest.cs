using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1;
using WebApplication1.Controllers;
using Gherkin.Ast;
using System.Linq;
using GherkinSpec;

namespace WebApplication1.Tests.Controllers
{
  [Feature(FilePath="Controllers/Feature.feature" /*FileAddress= "https://raw.githubusercontent.com/arekbal/arekbal.github.io/master/Feature.feature"*/) TestClass]
  public class HomeControllerTest : MsTestBase
  {
    public override void Background()
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

    [Scenario("Buy last coffee") TestMethod]
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

    [Scenario("Buy last coffee 2") TestMethod]
    public void Buy_last_coffe_2()
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
  }
}
