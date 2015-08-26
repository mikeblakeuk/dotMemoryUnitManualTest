// ReSharper disable once InconsistentNaming

using System.Threading;

public class DotMemoryUnitServerTestProgram : TestProgramBase
{
  public static readonly EventWaitHandle ExitEvent = new EventWaitHandle(false, EventResetMode.AutoReset, typeof(DotMemoryUnitServerTestProgram).Name);

  public static void Main(string[] args)
  {
    ProfilingApi.AssertProfilerIsConnected();
    ExitEvent.WaitOne();
  }
}