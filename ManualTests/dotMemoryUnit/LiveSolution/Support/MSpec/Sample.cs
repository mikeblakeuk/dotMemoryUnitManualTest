using JetBrains.dotMemoryUnit;
using JetBrains.dotMemoryUnit.Kernel;

[Subject("Algorithm uses memory effectively")]
public class when_I_aware_about_memory_effective_usage
{
  Because of_memory_should_be_used_effectively = () =>
  {
    using (interval = dotMemoryUnit.CollectDataOnInterval(true))
    {
      // code under the test
    }
  };

  It some_business_logic_assert = () =>
  {
    // some business logic value should be like it should be
  };

  It should_not_produce_more_than_1mb_traffic = () =>
  {
    if(dotMemoryUnit.IsEnabled) // in other case if run not under dotMemory Unit following call will fail
      interval
        .MemoryTraffic
        .AllocatedMemory
        .SizeInBytes
        .ShouldBeLessThan(1024*1024);
  };

  It should_not_create_too_many_strings = () =>
  {
    if(dotMemoryUnit.IsEnabled) // use it if test is run under dotMemory Unit and as usual unit tests
      interval
        .MemoryDifference
        .GetNewObjects()
        .GetObjects(where => where.Type.Is<string>())
        .ObjectsCount
        .ShouldBeLessThan(100);
  };

  static dotMemoryUnit.TimeInterval interval;
}

[Subject("Released objects collected")]
public class when_I_aware_about_memory_leak
{
  Because after_closing_ui = () =>
  {
    using(dotMemoryUnit.TestScope())
    {
      // open ui
        
      // close ui

      snapshot = dotMemoryUnit.GetSnapshot();
    }
  };

  It some_business_logic_assert = () =>
  {
    // some business logic value should be like it should be
  };

  It should_be_collected = () =>
  {
    if(dotMemoryUnit.IsEnabled)
      snapshot
        .GetObjects(where => where.Namespace.Like("MyNamespace.ViewModel*"))
        .ObjectsCount
        .ShouldEqual(0);
  };

  static Snapshot snapshot;
}