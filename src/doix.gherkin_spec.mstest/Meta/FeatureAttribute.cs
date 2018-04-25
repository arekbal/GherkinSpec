using doix.gherkin_spec.core.Loading;
using doix.gherkin_spec.core.Meta;
using System;

namespace doix.gherkin_spec.mstest.Meta
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public sealed class FeatureAttribute : Attribute, IFeatureAttribute
  {
    public Type Loader { get; set; }
    public string LoaderParam { get; set; }

    public string FileAddress
    {
      set
      {
        Loader = typeof(FeatureHttpLoader);
        LoaderParam = value;
      }
    }

    public string FilePath
    {
      set
      {
        Loader = typeof(FeatureFileLoader);
        LoaderParam = value;
      }
    }
  }
}
