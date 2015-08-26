using JetBrains.dotMemoryUnit;
using JetBrains.dotMemoryUnit.Kernel;
using Machine.Specifications;

namespace MSpec
{
  [Subject("TypeTests")]
  public class GenericTest
  {
    private static Snapshot _firstSnapshot;
    private static ObjectSet _objectSet;

    Establish contest = () =>
    {
      DotMemoryUnitController.TestStart();
      TypePropertyTestProgram.Execute(
          () => { _firstSnapshot = dotMemoryApi.GetSnapshot(); }
          );
    };

    Because of = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet = _firstSnapshot.GetObjects(where => where.Type.Is(TypePropertyTestProgram.Generic.Type));
    };

    It check_generic_type_objects_count = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.ObjectsCount.ShouldBeLike(TypePropertyTestProgram.Generic.Count);
    };

    It SizeInBytes_ShouldBeGreaterThan_0 = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.SizeInBytes.ShouldBeGreaterThan(0);
    };

    private Cleanup after = () =>
      DotMemoryUnitController.TestEnd();
  }

  [Subject("TypeTests")]
  public class ArrayTest
  {
    private static Snapshot _firstSnapshot;
    private static ObjectSet _objectSet;

    Establish contest = () =>
    {
      DotMemoryUnitController.TestStart();
      TypePropertyTestProgram.Execute(
          () => { _firstSnapshot = dotMemoryApi.GetSnapshot(); }
          );
    };

    Because of = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet = _firstSnapshot.GetObjects(where => where.Type.Is(TypePropertyTestProgram.Array.Type));
    };

    It check_array_objects_count = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.ObjectsCount.ShouldBeLike(TypePropertyTestProgram.Array.Count);
    };

    It SizeInBytes_ShouldBeGreaterThan_0 = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.SizeInBytes.ShouldBeGreaterThan(0);
    };

    private Cleanup after = () =>
      DotMemoryUnitController.TestEnd();
  }

  [Subject("TypeTests")]
  public class GenericArrayTest
  {
    private static Snapshot _firstSnapshot;
    private static ObjectSet _objectSet;

    Establish contest = () =>
    {
      DotMemoryUnitController.TestStart();
      TypePropertyTestProgram.Execute(
          () => { _firstSnapshot = dotMemoryApi.GetSnapshot(); }
          );
    };

    Because of = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet = _firstSnapshot.GetObjects(where => where.Type.Is(TypePropertyTestProgram.GenericArray.Type));
    };

    It check_generic_array_objects_count = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.ObjectsCount.ShouldBeLike(TypePropertyTestProgram.GenericArray.Count);
    };

    It SizeInBytes_ShouldBeGreaterThan_0 = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.SizeInBytes.ShouldBeGreaterThan(0);
    };

    private Cleanup after = () =>
      DotMemoryUnitController.TestEnd();
  }

  [Subject("TypeTests")]
  public class TypeIsTest
  {
    private static Snapshot _firstSnapshot;
    private static ObjectSet _objectSet;

    Establish contest = () =>
    {
      DotMemoryUnitController.TestStart();
      TypePropertyTestProgram.Execute(
          () => { _firstSnapshot = dotMemoryApi.GetSnapshot(); }
          );
    };

    Because of = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet = _firstSnapshot.GetObjects(where => where.Type.Is<TypePropertyTestProgram.One>());
    };

    It check_objects_count_by_type = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.ObjectsCount.ShouldBeLike(TypePropertyTestProgram.One.Count);
    };

    It SizeInBytes_ShouldBeGreaterThan_0 = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.SizeInBytes.ShouldBeGreaterThan(0);
    };

    private Cleanup after = () =>
      DotMemoryUnitController.TestEnd();
  }

  [Subject("TypeTests")]
  public class TypeIsListTest
  {
    private static Snapshot _firstSnapshot;
    private static ObjectSet _objectSet;

    Establish contest = () =>
    {
      DotMemoryUnitController.TestStart();
      TypePropertyTestProgram.Execute(
          () => { _firstSnapshot = dotMemoryApi.GetSnapshot(); }
          );
    };

    Because of = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet = _firstSnapshot.GetObjects(where => where.Type.Is(typeof(TypePropertyTestProgram.One), typeof(TypePropertyTestProgram.Two)));
    };

    It check_objects_count_by_type_list = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.ObjectsCount.ShouldBeLike(TypePropertyTestProgram.One.Count + TypePropertyTestProgram.Two.Count);
    };

    It SizeInBytes_ShouldBeGreaterThan_0 = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.SizeInBytes.ShouldBeGreaterThan(0);
    };

    private Cleanup after = () =>
      DotMemoryUnitController.TestEnd();
  }

  [Subject("TypeTests")]
  public class TypeIsNotTest
  {
    private static Snapshot _firstSnapshot;
    private static ObjectSet _objectSet;

    Establish contest = () =>
    {
      DotMemoryUnitController.TestStart();
      TypePropertyTestProgram.Execute(
          () => { _firstSnapshot = dotMemoryApi.GetSnapshot(); }
          );
    };

    Because of = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet = _firstSnapshot.GetObjects(where => where.Type.IsNot<TypePropertyTestProgram.One>());
    };

    It check_objects_count_by_is_not_type = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.ObjectsCount.ShouldBeLike(_firstSnapshot.ObjectsCount - TypePropertyTestProgram.One.Count);
    };

    It SizeInBytes_ShouldBeGreaterThan_0 = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.SizeInBytes.ShouldBeGreaterThan(0);
    };

    private Cleanup after = () =>
      DotMemoryUnitController.TestEnd();
  }

  [Subject("TypeTests")]
  public class TypeIsNotListTest
  {
    private static Snapshot _firstSnapshot;
    private static ObjectSet _objectSet;

    Establish contest = () =>
    {
      DotMemoryUnitController.TestStart();
      TypePropertyTestProgram.Execute(
          () => { _firstSnapshot = dotMemoryApi.GetSnapshot(); }
          );
    };

    Because of = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet = _firstSnapshot.GetObjects(where => where.Type.IsNot(typeof(TypePropertyTestProgram.One), typeof(TypePropertyTestProgram.Two)));
    };

    It check_objects_count_by_is_not_type_list = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.ObjectsCount.ShouldBeLike(_firstSnapshot.ObjectsCount - TypePropertyTestProgram.One.Count -
                                TypePropertyTestProgram.Two.Count);
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
