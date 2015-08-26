using System;
using System.Collections.Generic;
using JetBrains.dotMemoryUnit;

namespace Generic
{
  interface IFoo<T>
  {

  }

  class Foo<T> : IFoo<T>
  {

  }

  public class GenericTypeTests
  {

    public static void TestDictionaryType(Action<bool, string> assertTrue)
    {
      var obj = new Dictionary<int, List<GenericTypeTests>>();
      dotMemory.Check();
      dotMemory.Check(memory =>
      {
        var objectSet = memory.GetObjects(where => @where.Type.Is(obj.GetType()));
        assertTrue(objectSet.ObjectsCount == 1, objectSet.ToString());
        assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
      });
      GC.KeepAlive(obj);
    }

    public static void TestGenericInterface(Action<bool, string> assertTrue)
    {
      var foo = new Foo<string>();
      dotMemory.Check(memory =>
      {
        var objectSet = memory.GetObjects(where => where.Interface.Is<IFoo<string>>());
        assertTrue(objectSet.ObjectsCount == 1, objectSet.ToString());
        assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
      });
      GC.KeepAlive(foo);
    }

    public static void TestGeneric(Action<bool, string> assertTrue)
    {
      var foo = new Foo<string>();
      dotMemory.Check(memory =>
      {
        var objectSet = memory.GetObjects(where => where.Type.Is<Foo<string>>());
        assertTrue(objectSet.ObjectsCount == 1, objectSet.ToString());
        assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
      });
      GC.KeepAlive(foo);
    }

    public static void TestArray(Action<bool, string> assertTrue)
    {
      var foo = new Foo<string>[5];
      dotMemory.Check(memory =>
      {
        var objectSet = memory.GetObjects(where => where.Type.Is<Foo<string>[]>());
        assertTrue(objectSet.ObjectsCount == 1, objectSet.ToString());
        assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
      });
      GC.KeepAlive(foo);
    }

    public static void TestArrayInterface(Action<bool, string> assertTrue)
    {
      var foo = new Foo<string>[5];
      dotMemory.Check(memory =>
      {
        var objectSet = memory.GetObjects(where => where.Interface.Is<IFoo<string>>());
        assertTrue(objectSet.ObjectsCount == 1, objectSet.ToString());
        assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
      });
      GC.KeepAlive(foo);
    }

    public static void TestAssemblyForGenericType(Action<bool, string> assertTrue)
    {
      var foo = new Foo<int>();
      dotMemory.Check(memory =>
      {
        var objectSet = memory.GetObjects(where => where.Type.Is(typeof(Foo<int>)) & where.Assembly.Is(typeof(Foo<int>).Assembly));
        assertTrue(objectSet.ObjectsCount == 1, objectSet.ToString());
        assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
      });
      GC.KeepAlive(foo);
    }

    public static void TestAssemblyForDictionaryType(Action<bool, string> assertTrue)
    {
      var obj = new Dictionary<int, List<GenericTypeTests>>();
      var mscorlibAssembly = typeof(int).Assembly;

      dotMemory.Check(memory =>
      {
        var objectSet = memory.GetObjects(where => where.Assembly.Is(obj.GetType().Assembly));
        var objectSet2 = memory.GetObjects(where => where.Assembly.Is(mscorlibAssembly));
        var setByType = memory.GetObjects(where => where.Assembly.Is(obj.GetType().Assembly) & where.Type.Is(obj.GetType()));

        assertTrue(objectSet.ObjectsCount == objectSet2.ObjectsCount, objectSet.ToString() + "|" + objectSet2.ToString());
        assertTrue(objectSet.ObjectsCount > 0, objectSet2.ToString());
        assertTrue(setByType.ObjectsCount == 1, objectSet.ToString());
        assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
        assertTrue(objectSet2.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet2.SizeInBytes));
        assertTrue(setByType.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, setByType.SizeInBytes));
      });
      GC.KeepAlive(obj);
    }

    public static void TestAssemblyAndInterfaceQuery(Action<bool, string> assertTrue)
    {
      var foo = new Foo<string>();
      dotMemory.Check(memory =>
      {
        var objectSet = memory.GetObjects(where => where.Assembly.Is(typeof(Foo<string>).Assembly) & where.Interface.Is<IFoo<string>>());
        assertTrue(objectSet.ObjectsCount == 1, objectSet.ToString());
        assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
      });
      GC.KeepAlive(foo);
    }

    public static void TestAssemblyForArray(Action<bool, string> assertTrue)
    {
      var foo = new Foo<string>[5];
      dotMemory.Check(memory =>
      {
        var objectSet = memory.GetObjects(where => where.Assembly.Is(foo.GetType().Assembly) & where.Type.Is(foo.GetType()));
        assertTrue(objectSet.ObjectsCount == 1, objectSet.ToString());
        assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
      });
      GC.KeepAlive(foo);
    }
  }
}