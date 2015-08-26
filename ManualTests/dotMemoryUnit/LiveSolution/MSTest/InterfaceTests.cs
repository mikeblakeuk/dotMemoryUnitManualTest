using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest
{
  [TestClass]
  public class InterfaceTests
  {
    [TestMethod]
    public void IsTest()
    {
      Generic.InterfaceTests.IsTest(Assert.IsTrue);
    }

    [TestMethod]
    public void IsNotTest()
    {
      Generic.InterfaceTests.IsNotTest(Assert.IsTrue);
    }

    [TestMethod]
    public void IsNotTestWithFail()
    {
      Generic.InterfaceTests.IsNotTestWithFail(Assert.IsTrue);
    }

    [TestMethod]
    public void MultiInheritanceInterfaceIsTest()
    {
      Generic.InterfaceTests.MultiInheritanceInterfaceIsTest(Assert.IsTrue);
    }

    [TestMethod]
    public void IsListTest()
    {
      Generic.InterfaceTests.IsListTest(Assert.IsTrue);
    }

    [TestMethod]
    public void IsNotListTest()
    {
      Generic.InterfaceTests.IsNotListTest(Assert.IsTrue);
    }
  }
}