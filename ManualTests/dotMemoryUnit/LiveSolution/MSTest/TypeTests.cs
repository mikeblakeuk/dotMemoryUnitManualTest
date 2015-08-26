using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest
{
  [TestClass]
  public class TypeTests
  {
    [TestMethod]
    public void GenericTest()
    {
      Generic.TypeTests.GenericTest(Assert.IsTrue);
    }

    [TestMethod]
    public void ArrayTest()
    {
      Generic.TypeTests.ArrayTest(Assert.IsTrue);
    }

    [TestMethod]
    public void GenericArrayTest()
    {
      Generic.TypeTests.GenericArrayTest(Assert.IsTrue);
    }

    [TestMethod]
    public void TypeIsTest()
    {
      Generic.TypeTests.TypeIsTest(Assert.IsTrue);
    }

    [TestMethod]
    public void TypeIsListTest()
    {
      Generic.TypeTests.TypeIsListTest(Assert.IsTrue);
    }

    [TestMethod]
    public void TypeIsNotTest()
    {
      Generic.TypeTests.TypeIsNotTest(Assert.IsTrue);
    }

    [TestMethod]
    public void TypeIsNotListTest()
    {
      Generic.TypeTests.TypeIsNotListTest(Assert.IsTrue);
    }
  }
}
