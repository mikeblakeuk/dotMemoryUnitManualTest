using System;

// ReSharper disable once InconsistentNaming
public class GenerationPropertyTestProgram : TestProgramBase
{
  public static void Main(string[] args)
  {
    ProfilingApi.AssertProfilerIsConnected();
    ProfilingApi.EnableAllocations();

    Execute(() => ProfilingApi.GetSnapshot(), () => ProfilingApi.GetSnapshot(), () => ProfilingApi.GetSnapshot());
  }

  public static void Execute(Action firstSnapshot, Action secondSnapshot, Action thirdSnapshot)
  {
    Create<Gen1>(Gen1.Count);
    Create<Gen2>(Gen2.Count);
    var loh = Create<Loh>(Loh.Count);
    firstSnapshot(); //[0] should not be promoted upper than gen0
    GC.KeepAlive(loh);

    var gen1 = Create<Gen1>(Gen1.Count);
    secondSnapshot(); //[1] should be promoted to gen1

    var gen2 = Create<Gen2>(Gen2.Count);
    GcCollect();
    thirdSnapshot(); //[2] should be promoted to gen2

    GC.KeepAlive(gen1);
    GC.KeepAlive(gen2);
  }

  private static void GcCollect()
  {
    GC.Collect(2);
    GC.WaitForFullGCComplete();
  }

  public class Gen1
  {
    public const int Count = 32;
  }

  public class Gen2
  {
    public const int Count = 6;
  }

  public class Loh
  {
    public static readonly Type Type = typeof(int[]);
    public const int Count = 5;
    private readonly int[] myArray;

    public Loh()
    {
      myArray = new int[83000];
    }
  }
}