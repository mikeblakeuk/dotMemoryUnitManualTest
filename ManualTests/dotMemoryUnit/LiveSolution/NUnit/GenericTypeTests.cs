using NUnit.Framework;

namespace NUnit
{
  [TestFixture]
  public class GenericTypeTests
  {
    [Test]
    public void TestDictionaryType()
    {
      Generic.GenericTypeTests.TestDictionaryType(Assert.True);
    }

    [Test]
    public void TestGenericInterface()
    {
      Generic.GenericTypeTests.TestGenericInterface(Assert.True);
    }

    [Test]
    public void TestGeneric()
    {
      Generic.GenericTypeTests.TestGeneric(Assert.True);
    }

    [Test]
    public void TestGenericArray()
    {
      Generic.GenericTypeTests.TestArray(Assert.True);
    }

    [Test]
    public void TestGenericArrayInterface()
    {
      Generic.GenericTypeTests.TestArrayInterface(Assert.True);
    }

    [Test]
    public void TestAssemblyForGenericType()
    {

      Generic.GenericTypeTests.TestAssemblyForGenericType(Assert.True);
    }

    [Test]
    public void TestAssemblyForDictionaryType()
    {
      Generic.GenericTypeTests.TestAssemblyForDictionaryType(Assert.True);
    }

    [Test]
    public void TestAssemblyAndInterfaceQuery()
    {
      Generic.GenericTypeTests.TestAssemblyAndInterfaceQuery(Assert.True);
    }

    [Test]
    public void TestAssemblyForArray()
    {
      Generic.GenericTypeTests.TestAssemblyForArray(Assert.True);
    }
  }
}