using System;
using JetBrains.dotMemoryUnit;
using JetBrains.dotMemoryUnit.Kernel;

namespace Generic
{
  public static class GetDifferenceTests
  {
    private static Snapshot _getDiffFirstSnapshot;
    private static Snapshot _getDiffSecondSnapshot;
    private static MemoryCheckPoint _firstPoint;

    public static void DotMemoryApiGetDifference(Action<bool, string> assertThat)
    {
      GetDifferenceTestProgram.Execute(
          () =>
          {
            if (!dotMemoryApi.IsEnabled) return;
            _getDiffFirstSnapshot = dotMemoryApi.GetSnapshot();
          },
          () =>
          {
            if (!dotMemoryApi.IsEnabled) return;
            _getDiffSecondSnapshot = dotMemoryApi.GetSnapshot();
          }
          );

      if (!dotMemoryApi.IsEnabled) return;

      var deadObj =
          dotMemoryApi.GetDifference(_getDiffFirstSnapshot, _getDiffSecondSnapshot)
              .GetDeadObjects()
              .GetObjects(_ => _.Type.Is(typeof(FirstClass), typeof(SecondClass))).ObjectsCount;

      var newObj =
          dotMemoryApi.GetDifference(_getDiffFirstSnapshot, _getDiffSecondSnapshot)
              .GetNewObjects()
              .GetObjects(_ => _.Type.Is(typeof(FirstClass), typeof(SecondClass))).ObjectsCount;

      var survObj =
          dotMemoryApi.GetDifference(_getDiffFirstSnapshot, _getDiffSecondSnapshot)
              .GetSurvivedObjects()
              .GetObjects(_ => _.Type.Is(typeof(FirstClass), typeof(SecondClass))).ObjectsCount;


      assertThat(deadObj == 0, string.Format(AssertTemplates.AssertObjectsCountTemplate, 0, deadObj));
      assertThat(newObj == SecondClass.Count, string.Format(AssertTemplates.AssertObjectsCountTemplate, SecondClass.Count, newObj));
      assertThat(survObj == FirstClass.Count, string.Format(AssertTemplates.AssertObjectsCountTemplate, FirstClass.Count, survObj));
    }

    public static void DotMemoryGetDifference(Action<bool, string> assertThat)
    {
      GetDifferenceTestProgram.Execute(
          () =>
          {
            _firstPoint = dotMemory.Check();
          },
          () =>
          {
            dotMemory.Check(memory =>
            {
              var deadObj = memory
                  .GetDifference(_firstPoint)
                  .GetDeadObjects()
                  .GetObjects(_ => _.Type.Is(typeof(FirstClass), typeof(SecondClass))).ObjectsCount;

              var newObj = memory
                  .GetDifference(_firstPoint)
                  .GetNewObjects()
                  .GetObjects(_ => _.Type.Is(typeof(FirstClass), typeof(SecondClass))).ObjectsCount;

              var survObj = memory
                  .GetDifference(_firstPoint)
                  .GetSurvivedObjects()
                  .GetObjects(_ => _.Type.Is(typeof(FirstClass), typeof(SecondClass))).ObjectsCount;

              assertThat(deadObj == 0, string.Format(AssertTemplates.AssertObjectsCountTemplate, 0, deadObj));
              assertThat(newObj == SecondClass.Count, string.Format(AssertTemplates.AssertObjectsCountTemplate, SecondClass.Count, newObj));
              assertThat(survObj == FirstClass.Count, string.Format(AssertTemplates.AssertObjectsCountTemplate, FirstClass.Count, survObj));
            });
          });
    }
  }
}
