using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest
{
  [TestClass]
  public class GetDifferenceMethodTest
  {
    [TestMethod]
    public void TestForDotMemoryApiClass()
    {
      Generic.GetDifferenceTests.DotMemoryApiGetDifference(Assert.IsTrue);
    }

    [TestMethod]
    public void TestForDotMemoryClass()
    {
      Generic.GetDifferenceTests.DotMemoryGetDifference(Assert.IsTrue);
    }

  }
}
