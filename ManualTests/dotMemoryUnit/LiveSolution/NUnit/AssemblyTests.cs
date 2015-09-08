using NUnit.Framework;

namespace NUnit
{
  [TestFixture]
  public class AssemblyTests
  {
    [Test]
    public void LikeTest()
    {
      Generic.AssemblyTests.LikeTest(Assert.True);
    }

    [Test]
    public void IsTest()
    {
      Generic.AssemblyTests.IsTest(Assert.True);
    }

    [Test]
    public void NotLikeTest()
    {
      Generic.AssemblyTests.NotLikeTest(Assert.True);
    }

    [Test]
    public void IsNotTest()
    {
      Generic.AssemblyTests.IsNotTest(Assert.True);
    }

    [Test]
    public void IsNotByExclusionTest()
    {
      Generic.AssemblyTests.IsNotByExclusionTest(Assert.True);
    }

    [Test]
    public void ArrayTest()
    {
      Generic.AssemblyTests.ArrayTest(Assert.True);
    }

    [Test]
    public void ArrayNotInMscorlibTest()
    {
      Generic.AssemblyTests.ArrayNotInMscorlibTest(Assert.True);
    }

    [Test]
    public void EmptyIntersectionTest()
    {
      Generic.AssemblyTests.EmptyIntersectionTest(Assert.True);
    }

    [Test]
    public void GenericTypeIstest()
    {
      Generic.AssemblyTests.GenericTypeIstest(Assert.True);
    }

    [Test]
    public void FailTest()
    {
      Generic.AssemblyTests.FailTest(Assert.True);
    }

  }
}
