using System;
using JetBrains.dotMemoryUnit;
using JetBrains.dotMemoryUnit.TestPrograms;

namespace Generic
{
  public static class InterfaceTests
  {
    public static void IsTest(Action<bool, string> assertTrue)
    {
      InterfacePropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var objectSet = memory.GetObjects(_ => _.Interface.Is<InterfacePropertyTestProgram.I1>());

          assertTrue(objectSet.ObjectsCount == InterfacePropertyTestProgram.C12.Count,
            string.Format(AssertTemplates.AssertObjectsCountTemplate, InterfacePropertyTestProgram.C12.Count, objectSet.ObjectsCount));

          assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
        });
      },
      () => { }
      );
    }

    public static void IsNotTest(Action<bool, string> assertTrue)
    {
      InterfacePropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var objectSet = memory.GetObjects(_ => _.Interface.IsNot<InterfacePropertyTestProgram.I1>());

          assertTrue(objectSet.ObjectsCount == memory.ObjectsCount - InterfacePropertyTestProgram.C12.Count,
            string.Format(AssertTemplates.AssertObjectsCountTemplate, memory.ObjectsCount - InterfacePropertyTestProgram.C12.Count, objectSet.ObjectsCount));

          assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
        });
      },
      () => { }
      );
    }

    public static void IsNotTestWithFail(Action<bool, string> assertTrue)
    {
      InterfacePropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var objectSet = memory.GetObjects(_ => _.Interface.IsNot<InterfacePropertyTestProgram.I1>());

          assertTrue(objectSet.ObjectsCount == memory.ObjectsCount - InterfacePropertyTestProgram.C12.Count + 1,
            string.Format(AssertTemplates.AssertObjectsCountTemplate, memory.ObjectsCount - InterfacePropertyTestProgram.C12.Count, objectSet.ObjectsCount));

          assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
        });
      },
      () => { }
      );
    }

    public static void MultiInheritanceInterfaceIsTest(Action<bool, string> assertTrue)
    {
      InterfacePropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var objectSet = memory.GetObjects(_ => _.Interface.Is<InterfacePropertyTestProgram.I2>());

          const int expectedCount = InterfacePropertyTestProgram.C12.Count + InterfacePropertyTestProgram.C2.Count;
          assertTrue(objectSet.ObjectsCount == expectedCount, string.Format(AssertTemplates.AssertObjectsCountTemplate, expectedCount, objectSet.ObjectsCount));
          assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
        });
      },
      () => { }
      );
    }

    public static void IsListTest(Action<bool, string> assertTrue)
    {
      InterfacePropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var objectSet = memory.GetObjects(where => where.Interface.Is(typeof(InterfacePropertyTestProgram.I1), typeof(InterfacePropertyTestProgram.I2)));
          const int expectedCount = InterfacePropertyTestProgram.C12.Count + InterfacePropertyTestProgram.C2.Count;
          assertTrue(objectSet.ObjectsCount == expectedCount, string.Format(AssertTemplates.AssertObjectsCountTemplate, expectedCount, objectSet.ObjectsCount));
          assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
        });
      },
      () => { }
      );
    }

    public static void IsNotListTest(Action<bool, string> assertTrue)
    {
      InterfacePropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var objectSet = memory.GetObjects(where => where.Interface.IsNot(typeof(InterfacePropertyTestProgram.I1), typeof(InterfacePropertyTestProgram.I2)));
          var expectedCount = memory.ObjectsCount - InterfacePropertyTestProgram.C12.Count - InterfacePropertyTestProgram.C2.Count;
          assertTrue(objectSet.ObjectsCount == expectedCount, string.Format(AssertTemplates.AssertObjectsCountTemplate, expectedCount, objectSet.ObjectsCount));
          assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
        });
      },
      () => { }
      );
    }

    public static void KollectionTest(Type type, Action<bool, string> assertTrue)
    {
      InterfacePropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var objectSet = memory.GetObjects(where => where.Interface.Is(type));
          var expectedCount = InterfacePropertyTestProgram.KCollection.Count * 2;
          assertTrue(objectSet.ObjectsCount == expectedCount, string.Format(AssertTemplates.AssertObjectsCountTemplate, expectedCount, objectSet.ObjectsCount));
          assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
        });
      },
      () => { }
      );
    }
  }
}