using System;

// ReSharper disable once InconsistentNaming
public class dmuRetainedObjectSet
{
  public static void Main(string[] args)
  {
    var retained = Create<Retained>(7);

    var owner1 = new Owner1(retained[0], retained[1], retained[2], retained[5], retained[6]);
    var owner2 = new Owner2(retained[3], retained[4], retained[5], retained[6]);
    retained = null;

    ProfilingApi.GetSnapshot();

    GC.KeepAlive(owner1);
    GC.KeepAlive(owner2);
  }

  private static T[] Create<T>(int count) where T : new()
  {
    var array = new T[count];
    for (var i = 0; i < array.Length; i++)
      array[i] = new T();
    return array;
  }

  public class Owner1
  {
    public const int RetainedCount = 3;
    private readonly Retained[] myRetained;

    public Owner1(params Retained[] retained)
    {
      myRetained = retained;
    }
  }

  public class Owner2 : Owner1
  {
    public new const int RetainedCount = 2;
    public Owner2(params Retained[] retained) : base(retained)
    {}
  }

  public class Retained
  { }
}