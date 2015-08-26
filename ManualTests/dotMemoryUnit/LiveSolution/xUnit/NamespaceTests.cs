using JetBrains.dotMemoryUnit;
using Xunit;
using Xunit.Abstractions;

namespace XUnit
{

  public class NamespaceTests
  {
    public NamespaceTests(ITestOutputHelper atr)
    {
      DotMemoryUnitTestOutput.SetOutputMethod(atr.WriteLine);
    }

    [Fact]
    public void LikeTest()
    {
      Generic.NamespaceTests.LikeTest(Assert.True);
    }

    [Fact]
    public void NotLikeTest()
    {
      Generic.NamespaceTests.NotLikeTest(Assert.True);
    }
  }
}