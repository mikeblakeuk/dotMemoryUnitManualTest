using System;

// ReSharper disable once InconsistentNaming
public class dmuSaveWorkspace
{
  public static Type LohType = typeof (int[]);
  public static int LohTypeCount = 2;

  public static event EventHandler EventSource;

  public static void Main(string[] args)
  {
    ProfilingApi.AssertProfilerIsConnected();
    ProfilingApi.EnableAllocations();

    var a1 = Create(10, () => "sljg");
    ProfilingApi.GetSnapshot();

    var a2 = Create<int>(6);
    ProfilingApi.GetSnapshot();

    var lohType = Create(2, () => new int[30000]);
    ProfilingApi.GetSnapshot();

    var newOne = Create<long>(54);
    ProfilingApi.GetSnapshot();

    GC.KeepAlive(a1);
    GC.KeepAlive(a2);
    GC.KeepAlive(lohType);
    GC.KeepAlive(newOne);

  }

  public static T[] Create<T>(int count) where T : new()
  {
    return Create(count, () => new T());
  }
 
  public static T[] Create<T>(int count, Func<T> factory)
  {
    var array = new T[count];
    for (var i = 0; i < array.Length; i++)
      array[i] = factory();
    return array;
  }

}