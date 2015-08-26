using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest
{
  [TestClass]
  public class ObjectSetByGenerationTests
  {
    [TestMethod]
    public void LohTest()
    {
      Generic.ObjectSetByGenerationTests.LohTest(Assert.IsTrue);
    }

    [TestMethod]
    public void Gen0Test()
    {
      Generic.ObjectSetByGenerationTests.Gen0Test(Assert.IsTrue);
    }

    [TestMethod]
    public void Gen1Test()
    {
      Generic.ObjectSetByGenerationTests.Gen1Test(Assert.IsTrue);
    }

    [TestMethod]
    public void Gen2Test()
    {
      Generic.ObjectSetByGenerationTests.Gen2Test(Assert.IsTrue);
    }
  }
}