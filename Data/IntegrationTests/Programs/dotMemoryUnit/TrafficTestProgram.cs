using System;
using TrafficTestNamespace.LevelTwo;
using TrafficTest_Sibling;


namespace TrafficTestNamespace
{
  public class TrafficTestProgram : TestProgramBase
  {
    public static void Main(string[] args)
    {
      ProfilingApi.EnableAllocations();
      ProfilingApi.GetSnapshot();

      var one = Create<TrafficOne>(TrafficOne.LivedCount);
      var two = Create<TrafficTwo>(TrafficTwo.LivedCount);
      var three = Create<TrafficThree>(TrafficThree.LivedCount);
      var sibling = Create<TrafficSibling>(TrafficSibling.LivedCount);
      Create<TrafficOne>(TrafficOne.CollectedCount); // create and collect
      Create<TrafficTwo>(TrafficTwo.CollectedCount); // create and collect
      Create<TrafficThree>(TrafficThree.CollectedCount);
      Create<TrafficSibling>(TrafficSibling.CollectedCount);

      ProfilingApi.GetSnapshot();

      GC.KeepAlive(one);
      GC.KeepAlive(two);
      GC.KeepAlive(three);
      GC.KeepAlive(sibling);
    }

    public class TrafficOne : ITrafficOne
    {
      public const int LivedCount = 31;
      public const int CollectedCount = 23;
      public const int TotalCount = LivedCount + CollectedCount;
    }
  }

  namespace LevelTwo
  {
    public class TrafficTwo : ITrafficTwo
    {
      public const int LivedCount = 29;
      public const int CollectedCount = 8;
      public const int TotalCount = LivedCount + CollectedCount;
    }
  }
}

namespace TrafficTest_Sibling
{
  public class TrafficSibling : ITrafficOne, ITrafficTwo
  {
    public const int LivedCount = 2;
    public const int CollectedCount = 92;
    public const int TotalCount = LivedCount + CollectedCount;
  }
}

public class TrafficThree : ITrafficThree
{
  public const int LivedCount = 39;
  public const int CollectedCount = 54;
  public const int TotalCount = LivedCount + CollectedCount;
}

public interface ITrafficOne{}
public interface ITrafficTwo{}
public interface ITrafficThree{}