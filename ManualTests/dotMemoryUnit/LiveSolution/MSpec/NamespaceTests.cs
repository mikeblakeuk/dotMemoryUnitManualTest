
using JetBrains.dotMemoryUnit;
using JetBrains.dotMemoryUnit.Kernel;
using Machine.Specifications;
using NamespaceTest_LevelOne.LevelTwo;
using NamespaceTest_SiblingOne;

namespace MSpec
{
  [Subject("NamespaceTests")]
  public class LikeTest
  {
    private static Snapshot _firstSnapshot;
    private static ObjectSet _objectSet;

    Establish contest = () =>
    {
      DotMemoryUnitController.TestStart();
      NamespacePropertyTestProgram.Execute(
          () => { _firstSnapshot = dotMemoryApi.GetSnapshot(); }
          );
    };

    Because of = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet = _firstSnapshot.GetObjects(_ => _.Namespace.Like(typeof(Two).Namespace, typeof(Sibling).Namespace));
    };

    It type_Two_and_type_Sibling_Namespace_objCount = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.ObjectsCount.ShouldBeLike(Two.Count + Sibling.Count + 2);
    };

    It SizeInBytes_ShouldBeGreaterThan_0 = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.SizeInBytes.ShouldBeGreaterThan(0);
    };

    private Cleanup after = () =>
      DotMemoryUnitController.TestEnd();
  }

  [Subject("NamespaceTests")]
  public class NotLikeTest
  {
    private static Snapshot _firstSnapshot;
    private static ObjectSet _objectSet;

    Establish contest = () =>
    {
      DotMemoryUnitController.TestStart();
      NamespacePropertyTestProgram.Execute(
          () => { _firstSnapshot = dotMemoryApi.GetSnapshot(); }
          );
    };

    Because of = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet = _firstSnapshot.GetObjects(_ => _.Namespace.NotLike(typeof(Two).Namespace, typeof(Sibling).Namespace));
    };

    It objCount_except_type_Two_and_type_Sibling_Namespace = () =>
    {
      if (!dotMemoryApi.IsEnabled) return;
      _objectSet.ObjectsCount.ShouldBeLike(_firstSnapshot.ObjectsCount - (Two.Count + Sibling.Count + 2));
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