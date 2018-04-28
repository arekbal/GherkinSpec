using GherkinSpec.Core.Loading;
using GherkinSpec.Core.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace GherkinSpec.xUnit
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public sealed class FeatureAttribute : BeforeAfterTestAttribute, IFeatureAttribute
  {
    public Type Loader { get; set; }
    public string LoaderParam { get; set; }

    public string FileAddress // extras
    {
      set
      {
        Loader = typeof(FeatureHttpLoader);
        LoaderParam = value;
      }
    }

    public string FilePath // extras
    {
      set
      {
        Loader = typeof(FeatureFileLoader);
        LoaderParam = value;
      }
    }

    public override void Before(MethodInfo methodUnderTest)
    {
      var specBase = GherkinSpecBase.Curr;

      IXUnitGherkinSpecContextProvider specContextProvider = specBase;
      var specContext = specContextProvider.SpecContext;

      specContext.InitFeature(specBase);
      
      specBase.InternalOnInitScenario();
    
      specBase.InternalBackground();

      specContext.InitScenario(methodUnderTest.Name);
           
      base.Before(methodUnderTest);     
    }

    public override void After(MethodInfo methodUnderTest)
    {
      var specBase = GherkinSpecBase.Curr;

      IXUnitGherkinSpecContextProvider specContextProvider = specBase;
      var specContext = specContextProvider.SpecContext;
      
      base.After(methodUnderTest);
           
      specBase.InternalOnCleanupScenario(testPassed: true); // Not supported with xUnit currently... no known way to see during cleanup if test passed or not...

      specContext.CleanupScenario(testPassed: true);

      specContext.CleanupFeature();
    }
  }
}
