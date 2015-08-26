using JetBrains.dotMemoryUnit;
using Xunit;
using Xunit.Abstractions;

namespace XUnit
{

  public class GetDifferenceMethodTest
  {
    public GetDifferenceMethodTest(ITestOutputHelper atr)
    {
      DotMemoryUnitTestOutput.SetOutputMethod(atr.WriteLine);
    }
    [Fact]
    public void TestForDotMemoryApiClass()
    {
      Generic.GetDifferenceTests.DotMemoryApiGetDifference(Assert.True);
    }

    [Fact]
    public void TestForDotMemoryClass()
    {
      Generic.GetDifferenceTests.DotMemoryGetDifference(Assert.True);
    }

  }
}
