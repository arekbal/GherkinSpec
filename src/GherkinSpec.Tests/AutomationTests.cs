
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GherkinSpec.MsTest;
using System.Linq;
using GherkinSpec.Core.Loading;
using GherkinSpec.MsTest.Meta;

namespace GherkinSpec.Tests
{
  [TestClass]
  public class AutomationTests
  {
    [TestMethod]
    public void Automate()
    {
      var d = new Automation.AutomationRunner();
      d.Run();
    }

  }
}
