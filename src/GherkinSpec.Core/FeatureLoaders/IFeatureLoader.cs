﻿using Gherkin.Ast;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinSpec.Core.FeatureLoaders
{
  interface IFeatureLoader
  {
    TextReader ReadFeatureContent(string virtualPath);
  }
}