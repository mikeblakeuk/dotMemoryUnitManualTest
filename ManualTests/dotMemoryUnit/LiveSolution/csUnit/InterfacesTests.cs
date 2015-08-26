using System;
using System.Collections;
using System.Linq;

namespace csUnit
{
  [TestFixture]
  public class InterfacesTests
  {
    [Test]
    public void IsTest()
    {
      Generic.InterfaceTests.IsTest(Assert.True);
    }

    [Test]
    public void IsNotTest()
    {
      Generic.InterfaceTests.IsNotTest(Assert.True);
    }

    [Test]
    public void MultiInheritanceInterfaceIsTest()
    {
      Generic.InterfaceTests.MultiInheritanceInterfaceIsTest(Assert.True);
    }

    [Test]
    public void IsListTest()
    {
      Generic.InterfaceTests.IsListTest(Assert.True);
    }

    [Test]
    public void IsNotListTest()
    {
      Generic.InterfaceTests.IsNotListTest(Assert.True);
    }

//    private static IEnumerable KollectionTestCases()
//    {
//      return InterfacePropertyTestProgram.KCollection.Type.GetInterfaces().Where(_ =>
//        _.IsGenericType &&
//        (_.Name.Contains("ICollection") || _.Name.Contains("IList") || _.Name.Contains("IEnumerable")))
//        .Select(_ => new TestCaseData(_).SetName(_.Name));
//    }
//
//    [TestCaseSource("KollectionTestCases")]
//    public void KollectionTest(Type type)
//    {
//        Generic.InterfaceTests.KollectionTest(type, Assert.True);
//    }
  }
}