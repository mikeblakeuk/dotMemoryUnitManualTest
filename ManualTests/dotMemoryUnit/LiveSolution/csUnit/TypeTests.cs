namespace csUnit
{
  [TestFixture]
  public class TypeTests
  {
    [Test]
    public void GenericTest()
    {
      Generic.TypeTests.GenericTest(Assert.True);
    }

    [Test]
    public void ArrayTest()
    {
      Generic.TypeTests.ArrayTest(Assert.True);
    }

    [Test]
    public void GenericArrayTest()
    {
      Generic.TypeTests.GenericArrayTest(Assert.True);
    }

    [Test]
    public void TypeIsTest()
    {
      Generic.TypeTests.TypeIsTest(Assert.True);
    }

    [Test]
    public void TypeIsListTest()
    {
      Generic.TypeTests.TypeIsListTest(Assert.True);
    }

    [Test]
    public void TypeIsNotTest()
    {
      Generic.TypeTests.TypeIsNotTest(Assert.True);
    }

    [Test]
    public void TypeIsNotListTest()
    {
      Generic.TypeTests.TypeIsNotListTest(Assert.True);
    }
  }
}