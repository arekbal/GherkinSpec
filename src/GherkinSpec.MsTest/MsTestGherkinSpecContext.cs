using GherkinSpec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GherkinSpec.Core.FeatureWriting;

namespace GherkinSpec.MsTest
{
  public class MsTestGherkinSpecContext : GherkinSpecContext
  {
    readonly Func<IFeatureWriter> _writerFactory;

    public MsTestGherkinSpecContext(Func<IFeatureWriter> writerFactory)
    {
      _writerFactory = writerFactory;
    }

    protected override IFeatureWriter CreateWriter()
    {
      return _writerFactory();
    }
  }
}
