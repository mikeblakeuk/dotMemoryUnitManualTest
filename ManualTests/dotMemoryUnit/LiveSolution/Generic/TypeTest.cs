using System;
using JetBrains.dotMemoryUnit;

namespace Generic
{
  public static class TypeTest
  {
    public static void IsTest(Action<ObjectSet> assertAction)
    {
      TypePropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var objectSet = memory.GetObjects(where => where.Type.Is<TypePropertyTestProgram.One>());
          Console.WriteLine(objectSet);
          assertAction(objectSet);
        });
      });
    }
  }
}