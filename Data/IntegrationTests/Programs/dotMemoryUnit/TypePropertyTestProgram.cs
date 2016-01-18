using System;
using System.Collections.Generic;

namespace JetBrains.dotMemoryUnit.TestPrograms
{
  // ReSharper disable UnusedTypeParameter
  public class Foo<T1, T2, T3, T4>{ }


  public class TypePropertyTestProgram : TestProgramBase
  {
    public const int Live = 3;
    public const int Collected = 2;

    public static void Main(string[] args)
    {
      ProfilingApi.AssertProfilerIsConnected();
      ProfilingApi.EnableAllocations();
      Execute(() => ProfilingApi.GetSnapshot());
    }

    public static void Execute(Action getSnapshot)
    {
      Create<One>(One.Count);
      Create<Two>(Two.Count);

      var keepAlive = new List<object>
      {
        Create<One>(One.Count),
        Create<Two>(Two.Count),
        Create<Generic>(Generic.Count),
        Create<Array>(Array.Count),
        Create<GenericArray>(GenericArray.Count)
      };

      getSnapshot();

      // for traffic test
      keepAlive.AddRange(new object[]
      {
        Create<One>(One.Count * Live),
        Create<Two>(Two.Count * Live),
        Create<Generic>(Generic.Count * Live),
        Create<Array>(Array.Count * Live),
        Create<GenericArray>(GenericArray.Count * Live)
      });

      Create<One>(One.Count * Collected);
      Create<Two>(Two.Count * Collected);
      Create<Generic>(Generic.Count * Collected);
      Create<Array>(Array.Count * Collected);
      Create<GenericArray>(GenericArray.Count * Collected);

      getSnapshot();

      GC.KeepAlive(keepAlive);
    }

    private static System.Array CreateArrayInstance(Type type)
    {
      // ReSharper disable once AssignNullToNotNullAttribute
      return System.Array.CreateInstance(type.GetElementType(), new int[type.GetArrayRank()]);
    }

    public class Generic
    {
      public const int Count = 19;
      public static readonly Type Type = typeof (Foo<int, IEnumerable<Dictionary<List<int>, string>>, object, Dictionary<Foo<int, string, List<int>, Dictionary<bool, bool>>, Foo<List<List<object>>, bool, List<List<int>>, Foo<int, long, bool, string>>>>);
      public static readonly Type OpenType = typeof (Foo<,,,>);
      // ReSharper disable once NotAccessedField.Local
      private object myGenericObject;

      public Generic()
      {
        myGenericObject = Activator.CreateInstance(Type);
      }
    }

    public class Array
    {
      public const int Count = 7;
      public static readonly Type Type = typeof (string[,,][][,]);
      // ReSharper disable once NotAccessedField.Local
      private object myArrayObject;

      public Array()
      {
        myArrayObject = CreateArrayInstance(Type);
      }
    }

    public class GenericArray
    {
      public const int Count = 9;
      public static readonly Type Type = typeof (Foo<int, IEnumerable<Dictionary<List<int>, string>>, object, Dictionary<Foo<int, string, List<int>, Dictionary<bool, bool>>, Foo<List<List<object>>, bool, List<List<int>>, Foo<int, long, bool, string>>>>[][,,]);
      // ReSharper disable once NotAccessedField.Local
      private object myArrayObject;

      public GenericArray()
      {
        myArrayObject = CreateArrayInstance(Type);
      }
    }

    public class One
    {
      public const int Count = 32;
    }

    public class Two
    {
      public const int Count = 6;
    }
  }
}