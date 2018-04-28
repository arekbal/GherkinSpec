using GherkinSpec.Core.Loading;
using GherkinSpec.Core.Meta;
using System;

namespace GherkinSpec.NUnit.Meta
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
