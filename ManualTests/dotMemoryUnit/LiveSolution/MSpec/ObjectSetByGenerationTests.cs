using JetBrains.dotMemoryUnit;
using JetBrains.dotMemoryUnit.Kernel;
using JetBrains.dotMemoryUnit.Properties;
using Machine.Specifications;

namespace MSpec
{
  [Subject("ObjectSetByGenerationTests")]
  public class LohTest
  {
    private static Snapshot _firstSnapshot;
    private static ObjectSet _objectSet;

    Establish contest = () =>
    {
      DotMemoryUnitController.TestStart();
      GenerationPropertyTestProgram.Execute(
          () => { _firstSnapshot = dotMemoryApi.GetSnapshot(); },
          () => { }, () => { }
          );
    };

    Because of = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet = _firstSnapshot
          .GetObjects(_ => _.Generation.Is(Generation.LOH))
          .GetObjects(_ => _.Type.Is(GenerationPropertyTestProgram.Loh.Type));
    };

    It check_objects_count_by_Loh = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.ObjectsCount.ShouldBeLike(GenerationPropertyTestProgram.Loh.Count);
    };

    It SizeInBytes_ShouldBeGreaterThan_0 = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.SizeInBytes.ShouldBeGreaterThan(0);
    };

    private Cleanup after = () =>
      DotMemoryUnitController.TestEnd();
  }

  [Subject("ObjectSetByGenerationTests")]
  public class Gen0Test
  {
    private static Snapshot _firstSnapshot;
    private static ObjectSet _objectSet;

    Establish contest = () =>
    {
      DotMemoryUnitController.TestStart();
      GenerationPropertyTestProgram.Execute(
          () => { _firstSnapshot = dotMemoryApi.GetSnapshot(); },
          () => { }, () => { }
          );
    };

    Because of = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet = _firstSnapshot
          .GetObjects(where => where.Generation.Is(Generation.Gen1, Generation.Gen2, Generation.LOH))
          .GetObjects(where => where.Type.Is<GenerationPropertyTestProgram.Gen1>()
                      | where.Type.Is<GenerationPropertyTestProgram.Gen2>());
    };

    It check_objects_by_Gen1_Gen2_Loh_do_not_exist = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.ObjectsCount.ShouldBeLike(0);
    };

    It SizeInBytes_ShouldBeLike_0 = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.SizeInBytes.ShouldEqual(0);
    };

    private Cleanup after = () =>
      DotMemoryUnitController.TestEnd();
  }

  [Subject("ObjectSetByGenerationTests")]
  public class Gen1Test
  {
    private static Snapshot _secondSnapshot;
    private static ObjectSet _objectSet;

    Establish contest = () =>
    {
      DotMemoryUnitController.TestStart();
      GenerationPropertyTestProgram.Execute(
          () => { dotMemoryApi.GetSnapshot(); },
          () => { _secondSnapshot = dotMemoryApi.GetSnapshot(); },
          () => { }
          );
    };

    Because of = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet = _secondSnapshot
          .GetObjects(where => where.Generation.Is(Generation.Gen1))
          .GetObjects(where => where.Type.Is<GenerationPropertyTestProgram.Gen1>());
    };

    It check_objects_count_by_Gen1 = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.ObjectsCount.ShouldBeLike(GenerationPropertyTestProgram.Gen1.Count);
    };

    It SizeInBytes_ShouldBeGreaterThan_0 = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.SizeInBytes.ShouldBeGreaterThan(0);
    };

    private Cleanup after = () =>
      DotMemoryUnitController.TestEnd();
  }

  [Subject("ObjectSetByGenerationTests")]
  public class Gen2Test
  {
    private static Snapshot _thirdSnapshot;
    private static ObjectSet _objectSet;

    Establish contest = () =>
    {
      DotMemoryUnitController.TestStart();
      GenerationPropertyTestProgram.Execute(
          () => { dotMemoryApi.GetSnapshot(); },
          () => { dotMemoryApi.GetSnapshot(); },
          () => { _thirdSnapshot = dotMemoryApi.GetSnapshot(); }
          );
    };

    Because of = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet = _thirdSnapshot
          .GetObjects(where => where.Generation.Is(Generation.Gen2))
          .GetObjects(where => where.Type.Is<GenerationPropertyTestProgram.Gen1>()
                      | where.Type.Is<GenerationPropertyTestProgram.Gen2>());
    };

    It check_objects_count_by_Gen2 = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.ObjectsCount.ShouldBeLike(GenerationPropertyTestProgram.Gen1.Count + GenerationPropertyTestProgram.Gen2.Count);
    };

    It SizeInBytes_ShouldBeGreaterThan_0 = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.SizeInBytes.ShouldBeGreaterThan(0);
    };

    private Cleanup after = () =>
      DotMemoryUnitController.TestEnd();
  }
}