using System;

// ReSharper disable once InconsistentNaming
public class dmuTestProgram
{
  public static readonly Type LohType = typeof (int[]);
  public const int LohTypeCount = 2;

  public static event EventHandler EventSource;

  public static void Main(string[] args)
  {
    ProfilingApi.AssertProfilerIsConnected();
    ProfilingApi.EnableAllocations();

    var a1 = Create<One>(One.Count);
    var a2 = Create<Two>(Two.Count);
    var ns = Create<MyNamespace.SecondPart.dmuNamespaceTest>(MyNamespace.SecondPart.dmuNamespaceTest.Count);
    var lohType = Create(LohTypeCount, () => new int[30000]);

    Create<EventListener>(EventListener.Count);
    
//    GC.Collect();
//    GC.WaitForFullGCComplete();

    ProfilingApi.GetSnapshot();

    GC.KeepAlive(a1);
    GC.KeepAlive(a2);
    GC.KeepAlive(ns);
    GC.KeepAlive(lohType);

    var newOne = Create<One>(One.NewOneCount);

    Traffic surviver = null;
    for (var i = 0; i < Traffic.Count; i++)
    {
      surviver = new Traffic();
    }

    ProfilingApi.GetSnapshot();

    GC.KeepAlive(newOne);
    GC.KeepAlive(surviver);
  }

  public static T[] Create<T>(int count) where T : new()
  {
    var array = new T[count];
    for (var i = 0; i < array.Length; i++)
      array[i] = new T();
    return array;
  }

  public static T[] Create<T>(int count, Func<T> factory)
  {
    var array = new T[count];
    for (var i = 0; i < array.Length; i++)
      array[i] = factory();
    return array;
  }

  public interface IOne{}
  public class One : IOne
  {
    public const int Count = 1;
    public const int NewOneCount = 10;
  }

  public class Two
  {
    public const int Count = 65;
  }

  public class Traffic
  {
    public const int Count = 200;
  }
}



public class EventListener
{
  public const int Count = 4;
#pragma warning disable 414
  private int myField;
#pragma warning restore 414

  public EventListener()
  {
    dmuTestProgram.EventSource += (sender, args) => { myField = 5; };
  }
}

namespace MyNamespace.SecondPart
{
  public class dmuNamespaceTest
  {
    public const int Count = 3;
  }
}