using NUnit.Framework;

namespace NUnit
{
  public class TypeTest
  {
    [Test]
    public void IsTest()
    {
      Generic.TypeTest.IsTest(memoryInfo =>
      {
        Assert.That(memoryInfo.ObjectsCount, Is.EqualTo(TypePropertyTestProgram.One.Count - 1));
        Assert.That(memoryInfo.TotalSize, Is.GreaterThan(0));
      });
    }
  }
}