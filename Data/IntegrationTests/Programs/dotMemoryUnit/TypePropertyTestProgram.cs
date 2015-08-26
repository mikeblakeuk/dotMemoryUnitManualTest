using System;
using System.Collections.Generic;

// ReSharper disable UnusedTypeParameter
public class Foo<T1, T2, T3, T4>{ }
// ReSharper disable once UnusedTypeParameter

// ReSharper disable once InconsistentNaming
public class TypePropertyTestProgram : TestProgramBase
{
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

    var one = Create<One>(One.Count);
    var two = Create<Two>(Two.Count);
    var gen = Create<Generic>(Generic.Count);
    var arr = Create<Array>(Array.Count);
    var genarr = Create<GenericArray>(GenericArray.Count);

    getSnapshot();

    GC.KeepAlive(one);
    GC.KeepAlive(two);
    GC.KeepAlive(gen);
    GC.KeepAlive(arr);
    GC.KeepAlive(genarr);
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