using JetBrains.dotMemoryUnit;
using JetBrains.dotMemoryUnit.Kernel;
using JetBrains.dotMemoryUnit.TestPrograms;
using Machine.Specifications;

namespace MSpec
{
  [Subject("InterfaceTests")]
  public class IsTest
  {
    private static Snapshot _firstSnapshot;
    private static ObjectSet _objectSet;

    Establish contest = () =>
    {
      DotMemoryUnitController.TestStart();
      InterfacePropertyTestProgram.Execute(
          () => { _firstSnapshot = dotMemoryApi.GetSnapshot(); },
          () => { }
          );
    };

    Because of = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet = _firstSnapshot.GetObjects(_ => _.Interface.Is<InterfacePropertyTestProgram.I1>());
    };

    It check_objects_count_by_interface = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.ObjectsCount.ShouldBeLike(InterfacePropertyTestProgram.C12.Count);
    };

    It SizeInBytes_ShouldBeGreaterThan_0 = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.SizeInBytes.ShouldBeGreaterThan(0);
    };

    private Cleanup after = () =>
      DotMemoryUnitController.TestEnd();
  }

  [Subject("InterfaceTests")]
  public class IsNotTest
  {
    private static Snapshot _firstSnapshot;
    private static ObjectSet _objectSet;

    Establish contest = () =>
    {
      DotMemoryUnitController.TestStart();
      InterfacePropertyTestProgram.Execute(
        () => { _firstSnapshot = dotMemoryApi.GetSnapshot(); },
        () => { }
        );
    };

    Because of = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet = _firstSnapshot.GetObjects(_ => _.Interface.IsNot<InterfacePropertyTestProgram.I1>());
    };

    It check_objects_count_by_is_not_interface = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.ObjectsCount.ShouldBeLike(_firstSnapshot.ObjectsCount - InterfacePropertyTestProgram.C12.Count);
    };

    It SizeInBytes_ShouldBeGreaterThan_0 = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.SizeInBytes.ShouldBeGreaterThan(0);
    };

    private Cleanup after = () =>
      DotMemoryUnitController.TestEnd();
  }

  [Subject("InterfaceTests")]
  public class MultiInheritanceInterfaceIsTest
  {
    private static Snapshot _firstSnapshot;
    private static ObjectSet _objectSet;

    Establish contest = () =>
    {
      DotMemoryUnitController.TestStart();
      InterfacePropertyTestProgram.Execute(
          () => { _firstSnapshot = dotMemoryApi.GetSnapshot(); },
          () => { }
          );
    };

    Because of = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet = _firstSnapshot.GetObjects(_ => _.Interface.Is<InterfacePropertyTestProgram.I2>());
    };

    It check_objects_count_by_MultiInheritanceInterface = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.ObjectsCount.ShouldBeLike(InterfacePropertyTestProgram.C12.Count + InterfacePropertyTestProgram.C2.Count);
    };

    It SizeInBytes_ShouldBeGreaterThan_0 = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.SizeInBytes.ShouldBeGreaterThan(0);
    };

    private Cleanup after = () =>
      DotMemoryUnitController.TestEnd();
  }

  [Subject("InterfaceTests")]
  public class IsListTest
  {
    private static Snapshot _firstSnapshot;
    private static ObjectSet _objectSet;

    Establish contest = () =>
    {
      DotMemoryUnitController.TestStart();
      InterfacePropertyTestProgram.Execute(
          () => { _firstSnapshot = dotMemoryApi.GetSnapshot(); },
          () => { }
          );
    };

    Because of = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet = _firstSnapshot.GetObjects(where => where.Interface.Is(typeof(InterfacePropertyTestProgram.I1), typeof(InterfacePropertyTestProgram.I2)));
    };

    It check_objects_count_by_interface_list = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.ObjectsCount.ShouldBeLike(InterfacePropertyTestProgram.C12.Count + InterfacePropertyTestProgram.C2.Count);
    };

    It SizeInBytes_ShouldBeGreaterThan_0 = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.SizeInBytes.ShouldBeGreaterThan(0);
    };

    private Cleanup after = () =>
      DotMemoryUnitController.TestEnd();
  }

  [Subject("InterfaceTests")]
  public class IsNotListTest
  {
    private static Snapshot _firstSnapshot;
    private static ObjectSet _objectSet;

    Establish contest = () =>
    {
      DotMemoryUnitController.TestStart();
      InterfacePropertyTestProgram.Execute(
          () => { _firstSnapshot = dotMemoryApi.GetSnapshot(); },
          () => { }
          );
    };

    Because of = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet = _firstSnapshot.GetObjects(where => where.Interface.IsNot(typeof(InterfacePropertyTestProgram.I1), typeof(InterfacePropertyTestProgram.I2)));
    };

    It check_objects_count_by_is_not_interface_list = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.ObjectsCount.ShouldBeLike(_firstSnapshot.ObjectsCount - InterfacePropertyTestProgram.C12.Count - InterfacePropertyTestProgram.C2.Count);
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