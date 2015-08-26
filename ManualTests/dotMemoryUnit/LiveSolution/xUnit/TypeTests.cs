using JetBrains.dotMemoryUnit;
using Xunit;
using Xunit.Abstractions;

namespace XUnit
{


  [Collection("BeforeAllTestsRun")]
  public class TypeTests
  {
    public TypeTests(ITestOutputHelper atr)
    {
      DotMemoryUnitTestOutput.SetOutputMethod(atr.WriteLine);
    }

    [Fact]
    public void GenericTest()
    {
      Generic.TypeTests.GenericTest(Assert.True);
    }

    [Fact]
    public void ArrayTest()
    {
      Generic.TypeTests.ArrayTest(Assert.True);
    }

    [Fact]
    public void GenericArrayTest()
    {
      Generic.TypeTests.GenericArrayTest(Assert.True);
    }

    [Fact]
    public void TypeIsTest()
    {
      Generic.TypeTests.TypeIsTest(Assert.True);
    }

    [Fact]
    public void TypeIsListTest()
    {
      Generic.TypeTests.TypeIsListTest(Assert.True);
    }

    [Fact]
    public void TypeIsNotTest()
    {
      Generic.TypeTests.TypeIsNotTest(Assert.True);
    }

    [Fact]
    public void TypeIsNotListTest()
    {
      Generic.TypeTests.TypeIsNotListTest(Assert.True);
    }
  }
}
