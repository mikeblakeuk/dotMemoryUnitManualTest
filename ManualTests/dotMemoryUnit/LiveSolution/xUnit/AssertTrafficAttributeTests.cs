using JetBrains.dotMemoryUnit;
using Xunit;
using Xunit.Abstractions;

namespace XUnit
{
  public class AssertTrafficAttributeTests : Generic.AssertTrafficAttributeTests
  {
    public AssertTrafficAttributeTests(ITestOutputHelper atr)
    {
      DotMemoryUnitTestOutput.SetOutputMethod(atr.WriteLine);
    }

    [Fact]
    public override void InvalidMemoryAmountTest()
    {
      base.InvalidMemoryAmountTest();
    }

    [Fact]
    public override void InvalidObjectsCountTest()
    {
      base.InvalidObjectsCountTest();
    }

    [Fact]
    public override void InvalidNothingIsSet()
    {
      base.InvalidNothingIsSet();
    }

    [Fact]
    public override void AssertObjectsCountTest()
    {
      base.AssertObjectsCountTest();
    }

    [Fact]
    public override void AssertInvalidObjectsCountTest()
    {
      base.AssertInvalidObjectsCountTest();
    }
  }
}