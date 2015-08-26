using JetBrains.dotMemoryUnit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: EnableDotMemoryUnitSupport]
[assembly: DotMemoryUnit(FailIfRunWithoutSupport = false)]

namespace MSTest
{
  [TestClass]
  public class AssertTrafficAttributeTests : Generic.AssertTrafficAttributeTests
  {
    [TestMethod]
    public override void InvalidMemoryAmountTest()
    {
      base.InvalidMemoryAmountTest();
    }

    [TestMethod]
    public override void InvalidObjectsCountTest()
    {
      base.InvalidObjectsCountTest();
    }

    [TestMethod]
    public override void InvalidNothingIsSet()
    {
      base.InvalidNothingIsSet();
    }

    [TestMethod]
    public override void AssertObjectsCountTest()
    {
      base.AssertObjectsCountTest();
    }

    [TestMethod]
    public override void AssertInvalidObjectsCountTest()
    {
      base.AssertInvalidObjectsCountTest();
    }

    [TestMethod]
    public override void AssertObjectsCountByInterfaceTest()
    {
      base.AssertObjectsCountByInterfaceTest();
    }

    [TestMethod]
    public override void AssertObjectsCountByInterfaceInvalidTest()
    {
      base.AssertObjectsCountByInterfaceInvalidTest();
    }
  }
}