using NUnit.Framework;

namespace NUnit
{
  [TestFixture]
  public class GetDifferenceMethodTest
  {
    [Test]
    public void TestForDotMemoryApiClass()
    {
      Generic.GetDifferenceTests.DotMemoryApiGetDifference(Assert.That);
    }

    [Test]
    public void TestForDotMemoryClass()
    {
      Generic.GetDifferenceTests.DotMemoryGetDifference(Assert.That);
    }

  }
}
