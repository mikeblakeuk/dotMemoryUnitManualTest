using System;

namespace JetBrains.dotMemoryUnit
{
  internal class DotMemoryUnit : IDisposable
  {
    public static IDisposable Support
    {
      get { return new DotMemoryUnit(); }
    }

    private DotMemoryUnit()
    {
      DotMemoryUnitController.TestStart();
    }

    public void Dispose()
    {
      DotMemoryUnitController.TestEnd();
    }
  }
}