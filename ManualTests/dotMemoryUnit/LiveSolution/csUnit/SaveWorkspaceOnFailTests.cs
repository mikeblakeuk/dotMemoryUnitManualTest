namespace csUnit
{
  public class SaveWorkspaceOnFailTests : Generic.SaveWorkspaceOnFailTests
  {
    [Test]
    public void FailOnCheckTest()
    {
      FailOnCheckTest(Assert.Fail);
    }

    [Test]
    public override void DontSaveOnSimpleFailTest()
    {
      FailTest(Assert.Fail);
    }

    [Test]
    public override void DontSaveOnFailTest()
    {
      FailOnCheckTest(Assert.Fail);
    }

    [Test]
    public override void SaveOnSimpleFailTest()
    {
      FailTest(Assert.Fail);
    }
  }
}