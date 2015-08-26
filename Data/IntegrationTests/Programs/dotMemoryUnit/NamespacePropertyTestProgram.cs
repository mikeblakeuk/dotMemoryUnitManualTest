using System;
using NamespaceTest_LevelOne;
using NamespaceTest_LevelOne.LevelTwo;
using NamespaceTest_SiblingOne;

// ReSharper disable once InconsistentNaming
public class NamespacePropertyTestProgram : TestProgramBase
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
    Create<Sibling>(Sibling.Count);

    var one = Create<One>(One.Count);
    var two = Create<Two>(Two.Count);
    var sibling = Create<Sibling>(Sibling.Count);
    getSnapshot();

    GC.KeepAlive(one);
    GC.KeepAlive(two);
    GC.KeepAlive(sibling);
  }
}

namespace NamespaceTest_LevelOne
{
  public class One
  {
    public const int Count = 32;
  }

  namespace LevelTwo
  {
    public class Two
    {
      public const int Count = 6;
    }
  }
}

namespace NamespaceTest_SiblingOne
{
  public class Sibling
  {
    public const int Count = 27;
  }
}