using NUnit.Framework;

namespace NUnit
{
  [TestFixture]
  public class SaveOnFailOverrideTests : Generic.SaveOnFailOverrideTests
  {
    [Test]
    public void FailTest()
    {
      FailTest(Assert.Fail);
    }

    [Test]
    public override void FailOverrideTest()
    {
      FailTest(Assert.Fail);
    }
  }

  [TestFixture]
  public class SaveToAssemblyLocationTests : Generic.SaveToAssemblyLocationTests
  {
    [Test]
    public void FailTest()
    {
      FailTest(Assert.Fail);
    }
  }
}