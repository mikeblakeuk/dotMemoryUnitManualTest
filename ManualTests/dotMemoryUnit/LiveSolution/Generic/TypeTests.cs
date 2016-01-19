using System;
using JetBrains.dotMemoryUnit;
using JetBrains.dotMemoryUnit.TestPrograms;

namespace Generic
{
  public static class TypeTests
  {

    public static void GenericTest(Action<bool, string> assertTrue)
    {
      TypePropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var objectSet = memory.GetObjects(where => where.Type.Is(TypePropertyTestProgram.Generic.Type));
          Console.WriteLine(objectSet);

          assertTrue(objectSet.ObjectsCount == TypePropertyTestProgram.Generic.Count,
            string.Format(AssertTemplates.AssertObjectsCountTemplate, TypePropertyTestProgram.Generic.Count, objectSet.ObjectsCount));

          assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
        });
      },
      () => { }
      );
    }

    public static void ArrayTest(Action<bool, string> assertTrue)
    {
      TypePropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var objectSet = memory.GetObjects(where => where.Type.Is(TypePropertyTestProgram.Array.Type));
          Console.WriteLine(objectSet);

          assertTrue(objectSet.ObjectsCount == TypePropertyTestProgram.Array.Count,
            string.Format(AssertTemplates.AssertObjectsCountTemplate, TypePropertyTestProgram.Array.Count, objectSet.ObjectsCount));

          assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
        });
      },
      () => { }
      );
    }

    public static void GenericArrayTest(Action<bool, string> assertTrue)
    {
      TypePropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var objectSet = memory.GetObjects(where => where.Type.Is(TypePropertyTestProgram.GenericArray.Type));
          Console.WriteLine(objectSet);

          assertTrue(objectSet.ObjectsCount == TypePropertyTestProgram.GenericArray.Count,
            string.Format(AssertTemplates.AssertObjectsCountTemplate, TypePropertyTestProgram.GenericArray.Count, objectSet.ObjectsCount));

          assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
        });
      },
      () => { }
      );
    }

    public static void TypeIsTest(Action<bool, string> assertTrue)
    {
      TypePropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var objectSet = memory.GetObjects(where => where.Type.Is<TypePropertyTestProgram.One>());
          Console.WriteLine(objectSet);

          assertTrue(objectSet.ObjectsCount == TypePropertyTestProgram.One.Count,
            string.Format(AssertTemplates.AssertObjectsCountTemplate, TypePropertyTestProgram.One.Count, objectSet.ObjectsCount));

          assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
        });
      },
      () => { }
      );
    }

    public static void TypeIsListTest(Action<bool, string> assertTrue)
    {
      TypePropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var objectSet = memory.GetObjects(where => where.Type.Is(typeof(TypePropertyTestProgram.One), typeof(TypePropertyTestProgram.Two)));
          var expectedCount = TypePropertyTestProgram.One.Count + TypePropertyTestProgram.Two.Count;
          Console.WriteLine(objectSet);

          assertTrue(objectSet.ObjectsCount == expectedCount,
            string.Format(AssertTemplates.AssertObjectsCountTemplate, expectedCount, objectSet.ObjectsCount));

          assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
        });
      },
      () => { }
      );
    }

    public static void TypeIsNotTest(Action<bool, string> assertTrue)
    {
      TypePropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var objectSet = memory.GetObjects(where => where.Type.IsNot<TypePropertyTestProgram.One>());
          var expectedCount = memory.ObjectsCount - TypePropertyTestProgram.One.Count;
          Console.WriteLine(objectSet);

          assertTrue(objectSet.ObjectsCount == expectedCount,
            string.Format(AssertTemplates.AssertObjectsCountTemplate, expectedCount, objectSet.ObjectsCount));

          assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
        });
      },
      () => { }
      );
    }

    public static void TypeIsNotListTest(Action<bool, string> assertTrue)
    {
      TypePropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var objectSet = memory.GetObjects(where => where.Type.IsNot(typeof(TypePropertyTestProgram.One), typeof(TypePropertyTestProgram.Two)));
          var expectedCount = memory.ObjectsCount - TypePropertyTestProgram.One.Count -
                              TypePropertyTestProgram.Two.Count;
          Console.WriteLine(objectSet);

          assertTrue(objectSet.ObjectsCount == expectedCount,
            string.Format(AssertTemplates.AssertObjectsCountTemplate, expectedCount, objectSet.ObjectsCount));

          assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
        });
      },
      () => { }
      );
    }
  }
}