using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinSpec.Automation
{
  public class AutomationRunner
  {
    public void Run()
    {
      var parser = new Gherkin.Parser();
      var doc = parser.Parse("Feature.feature");

      var scenario = doc.Feature.Children.OfType<Gherkin.Ast.Scenario>().First(s => s.Tags.Any(t => t.Name == "@auto"));

      var service = OpenQA.Selenium.Chrome.ChromeDriverService.CreateDefaultService();

      using (var driver = new OpenQA.Selenium.Chrome.ChromeDriver(service))
      {
        //driver.Navigate();

        var tokenizer = new Tokenizer();

        foreach (var step in scenario.Steps)
        {
          tokenizer.Tokenize(step.Text);

          if (step.Text != tokenizer.AsText())
            throw new Exception("Parsed tokens dont sum up to input string");

          //tokenizer.Tokens.SkipWhile(t=> t.Kind != TokenKind.Word)

          Console.WriteLine("Hahah");
        }

        driver.Url = "http://www.google.pl?q=hell";

        var form = driver.FindElementById("tsf");
        form.Submit();

        Task.Delay(5000).Wait();
      }
    }

    

    public void GoTo(OpenQA.Selenium.Chrome.ChromeDriver driver, string address)
      => driver.Url = address;
  }
}
