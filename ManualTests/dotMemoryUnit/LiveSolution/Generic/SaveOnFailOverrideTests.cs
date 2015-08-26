using System;
using JetBrains.dotMemoryUnit;

[assembly: DotMemoryUnit(SavingStrategy = SavingStrategy.OnAnyFail, Directory = @"c:\tmp\Assembly")]

namespace Generic
{
  [DotMemoryUnit(SavingStrategy = SavingStrategy.OnAnyFail, Directory = @"..\Class")]
  public abstract class SaveOnFailOverrideTests
  {
    public static void FailTest(Action failAction)
    {
      dotMemory.Check(_ => failAction());
    }

    [DotMemoryUnit(SavingStrategy = SavingStrategy.OnCheckFail, Directory = @"..\Method")]
    public abstract void FailOverrideTest();

  }

  public abstract class SaveToAssemblyLocationTests
  {
    public static void FailTest(Action failAction)
    {
      dotMemory.Check(_ => failAction());
    }
  }
}
