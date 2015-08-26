using JetBrains.dotMemoryUnit;
using Machine.Specifications;

//[assembly: DotMemoryUnit(SavingStrategy = SavingStrategy.OnAnyFail, Directory = @"c:\tmp\Assembly")]

//Can't set DotMemoryUnitAttribute for a method
//Move SaveOnFailOverrideTests before SaveToAssemblyLocationTests class: DotMemoryUnitAttribute will not work for an assembly; it's global issue:
//https://youtrack.jetbrains.com/issue/DMRY-2779
namespace MSpec
{
  public class SaveToAssemblyLocationTests
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

    It save_on_fail_to_assembly_path = () =>
    {
      dotMemory.Check(_ => forFail.ShouldEqual(true));
    };

    private Cleanup after = () =>
      DotMemoryUnitController.TestEnd();
  }

  [DotMemoryUnit(SavingStrategy = SavingStrategy.OnAnyFail, Directory = @"C:\tmp\Class")]
  public class SaveOnFailOverrideTests
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

    It save_on_fail_to_class_path = () =>
    {
      dotMemory.Check(_ => forFail.ShouldEqual(true));
    };

    private Cleanup after = () =>
      DotMemoryUnitController.TestEnd();
  }
}