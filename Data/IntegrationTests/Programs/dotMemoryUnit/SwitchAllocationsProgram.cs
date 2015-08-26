using System;

namespace SwitchAllications
{
  public class SwitchAllocationsProgram : TestProgramBase
  {
    public static void Main(string[] args)
    {
      ProfilingApi.AssertProfilerIsConnected();
      ProfilingApi.EnableAllocations();

      //normal case
      ProfilingApi.GetSnapshot(); // 0
      var traffic0 = Create<TrafficObjects>(TrafficObjects.Count);
      ProfilingApi.GetSnapshot(); // 1

      // switch off/on before snapshots
      ProfilingApi.DisableAllocations();
      ProfilingApi.EnableAllocations();
      ProfilingApi.GetSnapshot(); // 2
      var traffic1 = Create<TrafficObjects>(TrafficObjects.Count);
      ProfilingApi.GetSnapshot(); // 3

      // switch off between snapshots
      ProfilingApi.GetSnapshot(); // 4
      ProfilingApi.DisableAllocations();
      var traffic2 = Create<TrafficObjects>(TrafficObjects.Count);
      ProfilingApi.GetSnapshot(); //5

      //switch on -> retrun to normal case
      ProfilingApi.EnableAllocations();
      ProfilingApi.GetSnapshot(); // 6
      var traffic3 = Create<TrafficObjects>(TrafficObjects.Count);
      ProfilingApi.GetSnapshot(); //7

      GC.KeepAlive(traffic0);
      GC.KeepAlive(traffic1);
      GC.KeepAlive(traffic2);
      GC.KeepAlive(traffic3);
    }
  }

  public class TrafficObjects
  {
    public static readonly int Count = 100;
  }
}