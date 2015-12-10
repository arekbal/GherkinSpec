using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace GherkinSpec.XUnit
{
  public class XUnitFeatureAttribute : BeforeAfterTestAttribute
  { 
    public override void Before(MethodInfo methodUnderTest)
    {
      base.Before(methodUnderTest);
            
      BeforeFired?.Invoke(methodUnderTest);
    }

    public event Action<MethodInfo> BeforeFired;

    public override void After(MethodInfo methodUnderTest)
    {
      base.After(methodUnderTest);

      AfterFired?.Invoke(methodUnderTest);
    }

    public event Action<MethodInfo> AfterFired;
  }
}
