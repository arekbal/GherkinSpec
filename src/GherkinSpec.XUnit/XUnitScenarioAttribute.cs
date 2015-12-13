﻿using GherkinSpec.Core.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace GherkinSpec.XUnit
{
  public class XUnitScenarioAttribute : FactAttribute, IScenarioAttribute
  {
    public string ScenarioName
    {
      get
      {
        return DisplayName;
      }
      set
      {
        DisplayName = value;
      }
    }
  }
}