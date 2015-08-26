using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest
{
  [TestClass]
  public class SaveWorkspaceOnFailTests : Generic.SaveWorkspaceOnFailTests
  {
    [TestMethod]
    public void FailOnCheckTest()
    {
      FailOnCheckTest(Assert.Fail);
    }

    [TestMethod]
    public override void DontSaveOnSimpleFailTest()
    {
      FailTest(Assert.Fail);
    }

    [TestMethod]
    public override void DontSaveOnFailTest()
    {
      FailOnCheckTest(Assert.Fail);
    }

    [TestMethod]
    public override void SaveOnSimpleFailTest()
    {
      FailTest(Assert.Fail);
    }
  }
}