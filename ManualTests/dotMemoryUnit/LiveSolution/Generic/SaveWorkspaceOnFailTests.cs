using System;
using JetBrains.dotMemoryUnit;

namespace Generic
{
  public abstract class SaveWorkspaceOnFailTests
  {
    public static void FailOnCheckTest(Action failAction)
    {
      dotMemory.Check(_ => failAction());
    }

    [DotMemoryUnit(SavingStrategy = SavingStrategy.OnCheckFail)]
    public abstract void DontSaveOnSimpleFailTest();

    [DotMemoryUnit(SavingStrategy = SavingStrategy.Never)]
    public abstract void DontSaveOnFailTest();

    [DotMemoryUnit(SavingStrategy = SavingStrategy.OnAnyFail)]
    public abstract void SaveOnSimpleFailTest();

    protected void FailTest(Action failAction)
    {
      failAction();
    }
  }
}