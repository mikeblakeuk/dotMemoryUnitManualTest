using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest
{
  [TestClass]
  public class NamespaceTests
  {
    [TestMethod]
    public void LikeTest()
    {
      Generic.NamespaceTests.LikeTest(Assert.IsTrue);
    }

    [TestMethod]
    public void NotLikeTest()
    {
      Generic.NamespaceTests.NotLikeTest(Assert.IsTrue);
    }
  }
}