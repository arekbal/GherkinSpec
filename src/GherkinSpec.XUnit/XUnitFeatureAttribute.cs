using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace GherkinSpec.XUnit
{
  public class XUnitFeatureAttribute : BeforeAfterTestAttribute
  { 
    public override void Before(MethodInfo methodUnderTest)
    {
      var specBase = GherkinSpecBase.Curr;

      IXUnitGherkinSpecContextProvider specContextProvider = specBase;

      var specContext = specContextProvider.SpecContext;

      specContext.InitFeature(specBase);

      specBase.Background();

      specContext.InitScenario(methodUnderTest.Name);

      base.Before(methodUnderTest);
    }

    public override void After(MethodInfo methodUnderTest)
    {
      var specBase = GherkinSpecBase.Curr;

      IXUnitGherkinSpecContextProvider specContextProvider = specBase;

      var specContext = specContextProvider.SpecContext;

      //specContext.CleanupScenario();

      specContext.CleanupFeature();

      base.After(methodUnderTest);      
    }
  }
}
