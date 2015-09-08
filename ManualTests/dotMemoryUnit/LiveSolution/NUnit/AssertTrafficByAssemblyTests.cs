using NUnit.Framework;

namespace NUnit
{
  [TestFixture]
  public class AssertTrafficByAssemblyTests
  {
    [Test]
    public void IsTest()
    {
      Generic.AssertTrafficByAssemblyTests.IsTest(Assert.True);
    }

    [Test]
    public void LikeTest()
    {
      Generic.AssertTrafficByAssemblyTests.LikeTest(Assert.True);
    }

    [Test]
    public void IsNotTest()
    {
      Generic.AssertTrafficByAssemblyTests.IsNotTest(Assert.True);
    }

    [Test]
    public void NotLikeTest()
    {
      Generic.AssertTrafficByAssemblyTests.NotLikeTest(Assert.True);
    }

    [Test]
    public void IsNotByExclusionTest()
    {
      Generic.AssertTrafficByAssemblyTests.IsNotByExclusionTest(Assert.True);
    }

    [Test]
    public void ArrayTest()
    {
      Generic.AssertTrafficByAssemblyTests.ArrayTest(Assert.True);
    }

    [Test]
    public void ArrayNotInMscorlibTest()
    {
      Generic.AssertTrafficByAssemblyTests.ArrayNotInMscorlibTest(Assert.True);
    }

    [Test]
    public void EmptyIntersectionTest()
    {
      Generic.AssertTrafficByAssemblyTests.EmptyIntersectionTest(Assert.True);
    }

    [Test]
    public void GenericTypeIsTest()
    {
      Generic.AssertTrafficByAssemblyTests.GenericTypeIsTest(Assert.True);
    }

    [Test]
    public void FailTest()
    {
      Generic.AssertTrafficByAssemblyTests.FailTest(Assert.True);
    }
  }
}