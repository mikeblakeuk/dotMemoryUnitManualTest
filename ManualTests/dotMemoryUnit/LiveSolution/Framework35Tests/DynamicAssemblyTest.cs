using JetBrains.dotMemoryUnit;
using NUnit.Framework;
using TestPrograms;

[assembly: EnableDotMemoryUnitSupport]
[assembly: DotMemoryUnit(FailIfRunWithoutSupport = false)]

namespace Framework35Tests
{
  class DynamicAssemblyTest
  {
    [Test]
    public static void DynamicAssembly()
    {
      DynamicAssemblyPprogram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var objectSet = memory.GetObjects(_ => _.Assembly.Like("MyModule"));
          Assert.True(objectSet.ObjectsCount == 1,
           string.Format("Expected objects count: {0}, but was: {1}", 1, objectSet.ObjectsCount));
          Assert.True(objectSet.SizeInBytes > 0, "Expected memory amount should be greater than 0, but was {0}", objectSet.SizeInBytes);
        });
      });
    }
  }
}
