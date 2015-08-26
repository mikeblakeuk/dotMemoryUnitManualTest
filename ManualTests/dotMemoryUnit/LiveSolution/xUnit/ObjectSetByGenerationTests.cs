using JetBrains.dotMemoryUnit;
using Xunit;
using Xunit.Abstractions;

namespace XUnit
{

  public class ObjectSetByGenerationTests
  {
    public ObjectSetByGenerationTests(ITestOutputHelper atr)
    {
      DotMemoryUnitTestOutput.SetOutputMethod(atr.WriteLine);
    }

    [Fact]
    public void LohTest()
    {
      Generic.ObjectSetByGenerationTests.LohTest(Assert.True);
    }

    [Fact]
    public void Gen0Test()
    {
      Generic.ObjectSetByGenerationTests.Gen0Test(Assert.True);
    }

    [Fact]
    public void Gen1Test()
    {
      Generic.ObjectSetByGenerationTests.Gen1Test(Assert.True);
    }

    [Fact]
    public void Gen2Test()
    {
      Generic.ObjectSetByGenerationTests.Gen2Test(Assert.True);
    }
  }
}