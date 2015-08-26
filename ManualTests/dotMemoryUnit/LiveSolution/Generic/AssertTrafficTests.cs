using System;
using JetBrains.dotMemoryUnit;

namespace Generic
{
  public abstract class AssertTrafficAttributeTests : TestProgramBase
  {
    [AssertTraffic()]
    public virtual void InvalidNothingIsSet()
    {

    }

    [AssertTraffic(AllocatedSizeInBytes = -1)]
    public virtual void InvalidMemoryAmountTest()
    {

    }

    [AssertTraffic(AllocatedObjectsCount = -1)]
    public virtual void InvalidObjectsCountTest()
    {

    }

    [AssertTraffic(AllocatedObjectsCount = AllocatableTraffic.Count, Types = new[] { typeof(AllocatableTraffic) })]
    public virtual void AssertObjectsCountTest()
    {
      var garbage = Create<AllocatableTraffic>(AllocatableTraffic.Count);
      if (garbage.Length < AllocatableTraffic.Count)
        throw new InvalidOperationException(); // preventing optimizations
    }

    [AssertTraffic(AllocatedObjectsCount = AllocatableTraffic.Count - 1, Types = new[] { typeof(AllocatableTraffic) })]
    public virtual void AssertInvalidObjectsCountTest()
    {
      var garbage = Create<AllocatableTraffic>(AllocatableTraffic.Count);
      if (garbage.Length < AllocatableTraffic.Count)
        throw new InvalidOperationException(); // preventing optimizations
    }

    [AssertTraffic(AllocatedObjectsCount = AllocatableTraffic.Count + AllocatableTrafficSecondType.Count, Interfaces = new[] { typeof(IAllocatableTraffic) })]
    public virtual void AssertObjectsCountByInterfaceTest()
    {
      var garbage = Create<AllocatableTraffic>(AllocatableTraffic.Count);
      if (garbage.Length < AllocatableTraffic.Count)
        throw new InvalidOperationException(); // preventing optimizations

      var garbage2 = Create<AllocatableTrafficSecondType>(AllocatableTrafficSecondType.Count);
      if (garbage2.Length < AllocatableTrafficSecondType.Count)
        throw new InvalidOperationException();
    }

    [AssertTraffic(AllocatedObjectsCount = AllocatableTraffic.Count + AllocatableTrafficSecondType.Count - 1, Interfaces = new[] { typeof(IAllocatableTraffic) })]
    public virtual void AssertObjectsCountByInterfaceInvalidTest()
    {
      var garbage = Create<AllocatableTraffic>(AllocatableTraffic.Count);
      if (garbage.Length < AllocatableTraffic.Count)
        throw new InvalidOperationException(); // preventing optimizations

      var garbage2 = Create<AllocatableTrafficSecondType>(AllocatableTrafficSecondType.Count);
      if (garbage2.Length < AllocatableTrafficSecondType.Count)
        throw new InvalidOperationException();
    }

    public class AllocatableTraffic : IAllocatableTraffic
    {
      public const int Count = 3;
    }

    public class AllocatableTrafficSecondType : IAllocatableTraffic
    {
      public const int Count = 2;
    }

    public interface IAllocatableTraffic
    {

    }
  }
}