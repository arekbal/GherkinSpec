using Xunit.Abstractions;
using System.Threading;
using GherkinSpec.Core;

namespace GherkinSpec.XUnit
{
  public class GherkinSpecBase : InternalGherkinSpecBase, IXUnitGherkinSpecContextProvider
  {
    readonly XUnitGherkinSpecContext _specContext;

    XUnitGherkinSpecContext IXUnitGherkinSpecContextProvider.SpecContext => _specContext;

    static AsyncLocal<GherkinSpecBase> _curr = new AsyncLocal<GherkinSpecBase>(); // Terrible Hack... but works... might be replaced with something better in the future

    internal static GherkinSpecBase Curr => _curr.Value;

    protected GherkinSpecBase(ITestOutputHelper outputHelper)
    {
      _curr.Value = this;

      _specContext = new XUnitGherkinSpecContext(outputHelper);
    }

    internal void InternalBackground()
      => Background();

    internal void InternalOnInitScenario()
      => OnInitScenario();

    internal void InternalOnCleanupScenario(bool testPassed)
      => OnCleanupScenario(testPassed);

    protected override sealed GherkinSpecContext GetSpecContext()
      => _specContext;
  }
}
