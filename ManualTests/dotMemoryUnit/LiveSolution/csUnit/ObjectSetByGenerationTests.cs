namespace csUnit
{
  [TestFixture]
  public class ObjectSetByGenerationTests
  {
    [Test]
    public void LohTest()
    {
      Generic.ObjectSetByGenerationTests.LohTest(Assert.True);
    }

    [Test]
    public void Gen0Test()
    {
      Generic.ObjectSetByGenerationTests.Gen0Test(Assert.True);
    }

    [Test]
    public void Gen1Test()
    {
      Generic.ObjectSetByGenerationTests.Gen1Test(Assert.True);
    }

    [Test]
    public void Gen2Test()
    {
      Generic.ObjectSetByGenerationTests.Gen2Test(Assert.True);
    }
  }
}