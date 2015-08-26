using System;
using JetBrains.dotMemoryUnit;
using Xunit;
using Xunit.Abstractions;

namespace XUnit
{

  public class SaveOnFailOverrideTests : Generic.SaveOnFailOverrideTests
  {
    public SaveOnFailOverrideTests(ITestOutputHelper atr)
    {
      DotMemoryUnitTestOutput.SetOutputMethod(atr.WriteLine);
    }

    [Fact]
    public void FailTest()
    {
      var failAction = new Action(assertFail);
      FailTest(failAction);
    }

    [Fact]
    public override void FailOverrideTest()
    {
      var failAction = new Action(assertFail);
      FailTest(failAction);
    }

    private void assertFail()
    {
      Assert.True(false);
    }
  }

  [Collection("BeforeAllTestsRun")]
  public class SaveToAssemblyLocationTests : Generic.SaveToAssemblyLocationTests
  {
    public SaveToAssemblyLocationTests(ITestOutputHelper atr)
    {
      DotMemoryUnitTestOutput.SetOutputMethod(atr.WriteLine);
    }

    [Fact]
    public void FailTest()
    {
      var failAction = new Action(assertFail);
      FailTest(failAction);
    }

    private void assertFail()
    {
      Assert.True(false);
    }
  }

}