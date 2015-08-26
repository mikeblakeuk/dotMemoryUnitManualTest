using JetBrains.dotMemoryUnit;
using Xunit;
using Xunit.Abstractions;

namespace XUnit
{

  public class InterfaceTests
  {
    public InterfaceTests(ITestOutputHelper atr)
    {
      DotMemoryUnitTestOutput.SetOutputMethod(atr.WriteLine);
    }

    [Fact]
    public void IsTest()
    {
      Generic.InterfaceTests.IsTest(Assert.True);
    }

    [Fact]
    public void IsNotTest()
    {
      Generic.InterfaceTests.IsNotTest(Assert.True);
    }

    [Fact]
    public void IsNotTestWithFail()
    {
      Generic.InterfaceTests.IsNotTestWithFail(Assert.True);
    }

    [Fact]
    public void MultiInheritanceInterfaceIsTest()
    {
      Generic.InterfaceTests.MultiInheritanceInterfaceIsTest(Assert.True);
    }

    [Fact]
    public void IsListTest()
    {
      Generic.InterfaceTests.IsListTest(Assert.True);
    }

    [Fact]
    public void IsNotListTest()
    {
      Generic.InterfaceTests.IsNotListTest(Assert.True);
    }
  }
}