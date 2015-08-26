using JetBrains.dotMemoryUnit;
using Machine.Specifications;

namespace MSpec
{
  [Subject("SaveWorkspaceOnFailTests")]
  public class FailOnCheckTest
  {
    private static bool forFail;

    Establish contest = () =>
    {
      DotMemoryUnitController.TestStart();
    };

    Because of = () =>
    {
      forFail = false;
    };

    It workspace_should_be_saved_after_check_fail = () =>
    {
      dotMemory.Check(_ => forFail.ShouldEqual(true));
    };

    private Cleanup after = () =>
      DotMemoryUnitController.TestEnd();
  };

  [Subject("SaveWorkspaceOnFailTests")]
  [DotMemoryUnit(SavingStrategy = SavingStrategy.OnCheckFail)]
  public class DontSaveOnSimpleFailTest
  {
    private static bool forFail;

    Establish contest = () =>
    {
      DotMemoryUnitController.TestStart();
    };

    Because of = () =>
    {
      forFail = false;
    };

    It workspace_shouldnt_be_saved_after_simple_fail = () =>
    {
      forFail.ShouldEqual(true);
    };

    private Cleanup after = () =>
      DotMemoryUnitController.TestEnd();
  };

  [Subject("SaveWorkspaceOnFailTests")]
  [DotMemoryUnit(SavingStrategy = SavingStrategy.Never)]
  public class DontSaveOnFailTest
  {
    private static bool forFail;

    Establish contest = () =>
    {
      DotMemoryUnitController.TestStart();
    };

    Because of = () =>
    {
      forFail = false;
    };

    It workspace_should_never_be_saved = () =>
    {
      dotMemory.Check(_ => forFail.ShouldEqual(true));
    };

    private Cleanup after = () =>
      DotMemoryUnitController.TestEnd();
  };
}