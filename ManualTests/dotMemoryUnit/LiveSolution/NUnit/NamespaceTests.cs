using NUnit.Framework;

namespace NUnit
{
  [TestFixture]
  public class NamespaceTests
  {
    [Test]
    public void LikeTest()
    {
      Generic.NamespaceTests.LikeTest(Assert.True);
    }

    [Test]
    public void NotLikeTest()
    {
      Generic.NamespaceTests.NotLikeTest(Assert.True);
    }
  }
}