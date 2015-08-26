using JetBrains.dotMemoryUnit;
using JetBrains.dotMemoryUnit.Kernel;
using Machine.Specifications;

namespace MSpec
{
  [Subject("GetDifferenceMethodTest")]
  public class TestForDotMemoryApiClass
  {
    private static Snapshot _getDiffFirstSnapshot;
    private static Snapshot _getDiffSecondSnapshot;
    private static int _deadObj;
    private static int _newObj;
    private static int _survObj;

    Establish contest = () =>
    {
      DotMemoryUnitController.TestStart();
      GetDifferenceTestProgram.Execute(
        () =>
        {
          _getDiffFirstSnapshot = dotMemoryApi.GetSnapshot();
        },
        () =>
        {
          _getDiffSecondSnapshot = dotMemoryApi.GetSnapshot();
        }
        );
    };

    Because of = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _deadObj =
        dotMemoryApi.GetDifference(_getDiffFirstSnapshot, _getDiffSecondSnapshot)
          .GetDeadObjects()
          .GetObjects(_ => _.Type.Is(typeof(FirstClass), typeof(SecondClass))).ObjectsCount;

      _newObj =
        dotMemoryApi.GetDifference(_getDiffFirstSnapshot, _getDiffSecondSnapshot)
          .GetNewObjects()
          .GetObjects(_ => _.Type.Is(typeof(FirstClass), typeof(SecondClass))).ObjectsCount;

      _survObj =
        dotMemoryApi.GetDifference(_getDiffFirstSnapshot, _getDiffSecondSnapshot)
          .GetSurvivedObjects()
          .GetObjects(_ => _.Type.Is(typeof(FirstClass), typeof(SecondClass))).ObjectsCount;
    };

    It dead_objects_count_should_be_like_0 = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _deadObj.ShouldBeLike(0);
    };

    It new_objects_count_should_be_like_SecondClass_Count = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _newObj.ShouldBeLike(SecondClass.Count);
    };

    It survived_objects_count_should_be_like_FirstClass_Count = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _survObj.ShouldBeLike(FirstClass.Count);
    };

    private Cleanup after = () =>
      DotMemoryUnitController.TestEnd();
  }

  [Subject("GetDifferenceMethodTest")]
  public class TestForDotMemoryClass
  {
    private static MemoryCheckPoint _getDiffFirstSnapshot;
    private static int _deadObj;
    private static int _newObj;
    private static int _survObj;

    Establish context = () =>
    {
      DotMemoryUnitController.TestStart();
      GetDifferenceTestProgram.Execute(
          () =>
          {
            _getDiffFirstSnapshot = dotMemory.Check();
          },
          () =>
          {
            dotMemory.Check(memory =>
            {
              _deadObj = memory
              .GetDifference(_getDiffFirstSnapshot)
              .GetDeadObjects()
              .GetObjects(_ => _.Type.Is(typeof(FirstClass), typeof(SecondClass))).ObjectsCount;

              _newObj = memory
              .GetDifference(_getDiffFirstSnapshot)
              .GetNewObjects()
              .GetObjects(_ => _.Type.Is(typeof(FirstClass), typeof(SecondClass))).ObjectsCount;

              _survObj = memory
              .GetDifference(_getDiffFirstSnapshot)
              .GetSurvivedObjects()
              .GetObjects(_ => _.Type.Is(typeof(FirstClass), typeof(SecondClass))).ObjectsCount;
            });
          });
    };

    Because of = () =>
    {

    };

    It dead_objects_count_should_be_like_0 = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _deadObj.ShouldBeLike(0);
    };

    It new_objects_count_should_be_like_SecondClass_Count = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _newObj.ShouldBeLike(SecondClass.Count);
    };

    It survived_objects_count_should_be_like_FirstClass_Count = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _survObj.ShouldBeLike(FirstClass.Count);
    };

    private Cleanup after = () =>
      DotMemoryUnitController.TestEnd();
  }
}
