using System;

public class GetDifferenceTestProgram : TestProgramBase
{
  public static void Main(string[] args)
  {
    ProfilingApi.AssertProfilerIsConnected();
    ProfilingApi.EnableAllocations();

    Execute(() => ProfilingApi.GetSnapshot(), () => ProfilingApi.GetSnapshot());
  }

  public static void Execute(Action getSnapshot1, Action getSnapshot2)
  {
    var class1Array = Create<FirstClass>(FirstClass.Count);
    getSnapshot1();

    var class2Array = Create<SecondClass>(SecondClass.Count);
    getSnapshot2();

    GC.KeepAlive(class1Array);
    GC.KeepAlive(class2Array);
  }
}

public class FirstClass
{
  public const int Count = 40;
}

public class SecondClass
{
  public const int Count = 10;
}