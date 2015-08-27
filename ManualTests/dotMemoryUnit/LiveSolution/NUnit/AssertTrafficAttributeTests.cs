using NUnit.Framework;

namespace NUnit
{
  [TestFixture]
  public class AssertTrafficAttributeTests : Generic.AssertTrafficAttributeTests
  {
    [Test]
    public override void InvalidMemoryAmountTest()
    {
      base.InvalidMemoryAmountTest();
    }

    [Test]
    public override void InvalidObjectsCountTest()
    {
      base.InvalidObjectsCountTest();
    }

    [Test]
    public override void InvalidNothingIsSet()
    {
      base.InvalidNothingIsSet();
    }

    [Test]
    public override void AssertObjectsCountTest()
    {
      base.AssertObjectsCountTest();
    }
  }
}