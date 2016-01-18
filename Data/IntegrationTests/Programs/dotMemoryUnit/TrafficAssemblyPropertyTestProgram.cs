using System;

// ReSharper disable once InconsistentNaming
public class TrafficAssemblyPropertyTestProgram : TestProgramBase
{
  public static void Main(string[] args)
  {
    ProfilingApi.AssertProfilerIsConnected();
    ProfilingApi.EnableAllocations();
    Execute(() => ProfilingApi.GetSnapshot());
  }

  public static void Execute(Action getSnapshot)
  {
    const string message = "SysGarbage was optimized";

    getSnapshot();
    var local = Create<LocalTraffic>(LocalTraffic.Count);
    var sysGarbage = Create<object>(Garbage.SystemCount);

    //preventing optimization 
    if (sysGarbage.Length < Garbage.SystemCount)
      throw new Exception(message);

    getSnapshot();

    var local1 = new LocalTraffic();

    getSnapshot();

    GC.KeepAlive(local);
    GC.KeepAlive(local1);
  }

  public class LocalTraffic
  {
    public const int Count = 37;
  }

  public class Garbage
  {
    public const int SystemCount = 4242;
  }

}