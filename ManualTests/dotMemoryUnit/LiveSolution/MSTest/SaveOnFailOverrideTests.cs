using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest
{
  [TestClass]
  public class SaveOnFailOverrideTests : Generic.SaveOnFailOverrideTests
  {
    [TestMethod]
    public void FailTest()
    {
      FailTest(Assert.Fail);
    }

    [TestMethod]
    public override void FailOverrideTest()
    {
      FailTest(Assert.Fail);
    }
  }

  [TestClass]
  public class SaveToAssemblyLocationTests : Generic.SaveToAssemblyLocationTests
  {
    [TestMethod]
    public void FailTest()
    {
      FailTest(Assert.Fail);
    }
  }

}