
//https://youtrack.jetbrains.com/issue/DMRY-2772
//I can't apply AssertTrafficAttribute because there're no test methods in MSpec  

using System;
using JetBrains.dotMemoryUnit;
using Machine.Specifications;

[assembly: DotMemoryUnit(FailIfRunWithoutSupport = false)]

namespace MSpec
{
  [Subject("AssertTrafficAttributeTests")]
  public class AssertObjectsCountTest : TestProgramBase
  {
    Because of = () =>
    {
      dotMemoryUnit.TimeInterval interval;
      using (interval = dotMemoryUnit.CollectDataOnInterval(true))
      {
        var garbage = Create<AllocatableTraffic>(AllocatableTraffic.Count);
        if (garbage.Length < AllocatableTraffic.Count)
          throw new InvalidOperationException();
      }
      memoryTraffic = interval.MemoryTraffic;
    };

    It check_allocatable_objects_count = () =>
    {
      if(dotMemoryUnit.IsEnabled)
        memoryTraffic
          .Where(w => w.Type.Is<AllocatableTraffic>())
          .AllocatedMemory
          .ObjectsCount
          .ShouldEqual(AllocatableTraffic.Count);
    };

    It check_AllocatedMemory = () =>
    {
      if (dotMemoryUnit.IsEnabled)
        memoryTraffic
          .AllocatedMemory
          .SizeInBytes
          .ShouldBeGreaterThan(0);
    };

    It check_CollectedMemory = () =>
    {
      if (dotMemoryUnit.IsEnabled)
        memoryTraffic
          .CollectedMemory
          .SizeInBytes
          .ShouldBeGreaterThan(0);

    };

    static Traffic memoryTraffic;
  }

  public class AllocatableTraffic
  {
    public const int Count = 3;
  }
}