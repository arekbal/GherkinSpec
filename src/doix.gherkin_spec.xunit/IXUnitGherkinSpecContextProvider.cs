using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doix.gherkin_spec.xunit
{
  interface IXUnitGherkinSpecContextProvider
  {
    XUnitGherkinSpecContext SpecContext { get; }
  }
}
