using System;


// ReSharper disable once InconsistentNaming
public class AssemblyPropertyTestProgram : TestProgramBase
{
  public static void Main(string[] args)
  {
    ProfilingApi.AssertProfilerIsConnected();
    ProfilingApi.EnableAllocations();
    Execute(() => ProfilingApi.GetSnapshot());
  }

  public static void Execute(Action getSnapshot)
  {
    Create<Local>(Local.Count);

    var local = Create<Local>(Local.Count);

    getSnapshot();

    Create<Local>(Local.Count);

    var local1 = Create<Local>(Local.Count);
    var sysObj = new object(); 
    getSnapshot();

    GC.KeepAlive(local);
    GC.KeepAlive(local1);
    GC.KeepAlive(sysObj);
  }

  public class Local
  {
    public const int Count = 37;
  }
  
}