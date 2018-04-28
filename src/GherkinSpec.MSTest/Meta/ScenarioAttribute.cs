﻿using GherkinSpec.Core.Meta;
using System;

namespace GherkinSpec.MSTest.Meta
{
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
  public sealed class ScenarioAttribute : Attribute, IScenarioAttribute
  {
    public string ScenarioName { get; }

    public ScenarioAttribute(string scenarioName = null)
    {
      ScenarioName = scenarioName;
    }
  }
}
