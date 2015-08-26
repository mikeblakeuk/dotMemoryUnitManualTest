using System;
using JetBrains.dotMemoryUnit;
using Xunit;
using Xunit.Abstractions;

namespace XUnit
{

  public class SaveWorkspaceOnFailTests : Generic.SaveWorkspaceOnFailTests
  {
    public SaveWorkspaceOnFailTests(ITestOutputHelper atr)
    {
      DotMemoryUnitTestOutput.SetOutputMethod(atr.WriteLine);
    }

    [Fact]
    public void FailOnCheckTest()
    {
      var failAction = new Action(assertFail);
      FailOnCheckTest(failAction);
    }

    [Fact]
    public override void DontSaveOnSimpleFailTest()
    {
      var failAction = new Action(assertFail);
      FailTest(failAction);
    }

    [Fact]
    public override void DontSaveOnFailTest()
    {
      var failAction = new Action(assertFail);
      FailOnCheckTest(failAction);
    }

    [Fact]
    public override void SaveOnSimpleFailTest()
    {
      var failAction = new Action(assertFail);
      FailOnCheckTest(failAction);
    }

    private void assertFail()
    {
      Assert.True(false);
    }
  }
}