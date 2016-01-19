using System;
using System.Collections.Generic;
using JetBrains.dotMemoryUnit;
using JetBrains.dotMemoryUnit.Kernel;
using JetBrains.dotMemoryUnit.TestPrograms;

namespace Generic
{
  public class AssertTrafficByAssemblyTests : TestProgramBase
  {
    private static IEnumerable<string> GetLikePatterns()
    {
      var fullName = typeof(AllocatableTraffic).Assembly.FullName;
      yield return fullName;
      yield return fullName.Substring(0, 5) + "*";
      yield return "*" + fullName.Substring(5, fullName.Length - 5);

      var shortName = fullName.Substring(0, fullName.IndexOf(",", StringComparison.Ordinal));
      yield return shortName + "*";
      yield return shortName.Substring(0, 5) + "*";
      yield return "*" + shortName.Substring(1, shortName.Length - 2) + "*";
    }

    public static void IsTest(Action<bool, string> assertTrue)
    {
      if (dotMemoryApi.IsEnabled)
        dotMemoryApi.CollectAllocations = true;

      var checkpoint1 = dotMemory.Check();
      var garbage = Create<AllocatableTraffic>(AllocatableTraffic.Count);
      if (garbage.Length < AllocatableTraffic.Count)
        throw new InvalidOperationException();

      dotMemory.Check(_ =>
      {
        var allocatedObjects =
          _.GetTrafficFrom(checkpoint1)
            .Where(w => w.Assembly.Is(typeof(AllocatableTraffic).Assembly))
            .AllocatedMemory;

        assertTrue(allocatedObjects.ObjectsCount <= AllocatableTraffic.Count + 1,
          string.Format(AssertTemplates.AssertObjectsLessOrEqualTemplate, AllocatableTraffic.Count + 1, allocatedObjects.ObjectsCount));
        assertTrue(allocatedObjects.SizeInBytes > 0,
          string.Format(AssertTemplates.AssertSizeInBytesTemplate, allocatedObjects.SizeInBytes));
      });
    }

    public static void LikeTest(Action<bool, string> assertTrue)
    {
      if (dotMemoryApi.IsEnabled)
        dotMemoryApi.CollectAllocations = true;

      var checkpoint1 = dotMemory.Check();
      var garbage = Create<AllocatableTraffic>(AllocatableTraffic.Count);
      if (garbage.Length < AllocatableTraffic.Count)
        throw new InvalidOperationException(); // preventing optimizations

      dotMemory.Check(_ =>
      {
        var patterns = GetLikePatterns();
        foreach (var pattern in patterns)
        {
          Console.WriteLine(pattern);
          var allocatedObjects =
            _.GetTrafficFrom(checkpoint1)
              .Where(w => w.Assembly.Like(pattern))
              .AllocatedMemory;

          assertTrue(allocatedObjects.ObjectsCount <= AllocatableTraffic.Count + 1,
            string.Format(AssertTemplates.AssertObjectsLessOrEqualTemplate, AllocatableTraffic.Count + 1, allocatedObjects.ObjectsCount));
          assertTrue(allocatedObjects.SizeInBytes > 0,
            string.Format(AssertTemplates.AssertSizeInBytesTemplate, allocatedObjects.SizeInBytes));
        }
      });
    }

    public static void IsNotTest(Action<bool, string> assertTrue)
    {
      if (dotMemoryApi.IsEnabled)
        dotMemoryApi.CollectAllocations = true;

      var checkpoint1 = dotMemory.Check();
      var garbage = Create<AllocatableTraffic>(AllocatableTraffic.Count);
      if (garbage.Length < AllocatableTraffic.Count)
        throw new InvalidOperationException(); // preventing optimizations

      dotMemory.Check(_ =>
      {
        var totalObjects = _.GetTrafficFrom(checkpoint1).AllocatedMemory.ObjectsCount;
        var allocatedObjects =
          _.GetTrafficFrom(checkpoint1)
            .Where(w => w.Assembly.IsNot(typeof(AllocatableTraffic).Assembly))
            .AllocatedMemory;

        assertTrue(allocatedObjects.ObjectsCount <= totalObjects - AllocatableTraffic.Count - 1,
          string.Format(AssertTemplates.AssertObjectsLessOrEqualTemplate, totalObjects - AllocatableTraffic.Count - 1, allocatedObjects.ObjectsCount));
        assertTrue(allocatedObjects.SizeInBytes > 0,
          string.Format(AssertTemplates.AssertSizeInBytesTemplate, allocatedObjects.SizeInBytes));
      });
    }

    public static void NotLikeTest(Action<bool, string> assertTrue)
    {
      if (dotMemoryApi.IsEnabled)
        dotMemoryApi.CollectAllocations = true;

      var checkpoint1 = dotMemory.Check();
      var garbage = Create<AllocatableTraffic>(AllocatableTraffic.Count);
      if (garbage.Length < AllocatableTraffic.Count)
        throw new InvalidOperationException(); // preventing optimizations

      dotMemory.Check(_ =>
      {
        var patterns = GetLikePatterns();
        var totalObjects = _.GetTrafficFrom(checkpoint1).AllocatedMemory.ObjectsCount;
        foreach (var pattern in patterns)
        {
          Console.WriteLine(pattern);
          var allocatedObjects =
            _.GetTrafficFrom(checkpoint1)
              .Where(w => w.Assembly.NotLike(pattern))
              .AllocatedMemory;

          assertTrue(allocatedObjects.ObjectsCount <= totalObjects - AllocatableTraffic.Count - 1,
            string.Format(AssertTemplates.AssertObjectsLessOrEqualTemplate, totalObjects - AllocatableTraffic.Count - 1, allocatedObjects.ObjectsCount));
          assertTrue(allocatedObjects.SizeInBytes > 0,
            string.Format(AssertTemplates.AssertSizeInBytesTemplate, allocatedObjects.SizeInBytes));
        }
      });
    }

    public static void IsNotByExclusionTest(Action<bool, string> assertTrue)
    {
      if (dotMemoryApi.IsEnabled)
        dotMemoryApi.CollectAllocations = true;

      var checkpoint1 = dotMemory.Check();
      var garbage = Create<AllocatableTraffic>(AllocatableTraffic.Count);
      if (garbage.Length < AllocatableTraffic.Count)
        throw new InvalidOperationException(); // preventing optimizations

      var mscorlibAssembly = typeof(int).Assembly;
      dotMemory.Check(_ =>
      {
        var totalObjects = _.GetTrafficFrom(checkpoint1).AllocatedMemory;
        var isAllocatedObjects =
          _.GetTrafficFrom(checkpoint1)
            .Where(w => w.Assembly.Is(mscorlibAssembly))
            .AllocatedMemory;
        var isNotAllocatedObjects =
          _.GetTrafficFrom(checkpoint1)
            .Where(w => w.Assembly.IsNot(mscorlibAssembly))
            .AllocatedMemory;

        assertTrue(isNotAllocatedObjects.ObjectsCount == totalObjects.ObjectsCount - isAllocatedObjects.ObjectsCount,
          string.Format(AssertTemplates.AssertObjectsCountTemplate, totalObjects.ObjectsCount - isAllocatedObjects.ObjectsCount, isNotAllocatedObjects.ObjectsCount));
        assertTrue(isNotAllocatedObjects.SizeInBytes == totalObjects.SizeInBytes - isAllocatedObjects.SizeInBytes,
            string.Format(AssertTemplates.AssertExactTotalSizeTemplate, totalObjects.SizeInBytes - isAllocatedObjects.SizeInBytes, isNotAllocatedObjects.SizeInBytes));
      });
    }

    public static void ArrayTest(Action<bool, string> assertTrue)
    {
      if (dotMemoryApi.IsEnabled)
        dotMemoryApi.CollectAllocations = true;

      var checkpoint1 = dotMemory.Check();
      var garbage = Create<AllocatableTraffic>(AllocatableTraffic.Count);
      if (garbage.Length < AllocatableTraffic.Count)
        throw new InvalidOperationException(); // preventing optimizations

      dotMemory.Check(_ =>
      {
        var allocatedObjects =
          _.GetTrafficFrom(checkpoint1)
            .Where(w => w.Assembly.Is(typeof(AllocatableTraffic[]).Assembly) & w.Type.Is<AllocatableTraffic[]>())
            .AllocatedMemory;

        assertTrue(allocatedObjects.ObjectsCount == 1,
           string.Format(AssertTemplates.AssertObjectsCountTemplate, 1, allocatedObjects.ObjectsCount));
        assertTrue(allocatedObjects.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, allocatedObjects.SizeInBytes));
      });
    }

    public static void ArrayNotInMscorlibTest(Action<bool, string> assertTrue)
    {
      if (dotMemoryApi.IsEnabled)
        dotMemoryApi.CollectAllocations = true;

      var checkpoint1 = dotMemory.Check();
      var garbage = Create<AllocatableTraffic>(AllocatableTraffic.Count);
      if (garbage.Length < AllocatableTraffic.Count)
        throw new InvalidOperationException(); // preventing optimizations

      var mscorlibAssembly = typeof(int).Assembly;
      dotMemory.Check(_ =>
      {
        var allocatedObjects =
          _.GetTrafficFrom(checkpoint1)
            .Where(w => w.Assembly.Is(mscorlibAssembly) & w.Type.Is<AllocatableTraffic[]>())
            .AllocatedMemory;

        assertTrue(allocatedObjects.ObjectsCount == 0,
           string.Format(AssertTemplates.AssertObjectsCountTemplate, 0, allocatedObjects.ObjectsCount));
        assertTrue(allocatedObjects.SizeInBytes == 0, string.Format(AssertTemplates.AssertExactTotalSizeTemplate, 0, allocatedObjects.SizeInBytes));
      });
    }

    public static void EmptyIntersectionTest(Action<bool, string> assertTrue)
    {
      if (dotMemoryApi.IsEnabled)
        dotMemoryApi.CollectAllocations = true;

      var checkpoint1 = dotMemory.Check();
      var garbage = Create<AllocatableTraffic>(AllocatableTraffic.Count);
      if (garbage.Length < AllocatableTraffic.Count)
        throw new InvalidOperationException(); // preventing optimizations

      var mscorlibAssembly = typeof(int).Assembly;
      dotMemory.Check(_ =>
      {
        var allocatedObjects =
          _.GetTrafficFrom(checkpoint1)
            .Where(w => w.Assembly.Is(mscorlibAssembly) & w.Type.Is<AllocatableTraffic>())
            .AllocatedMemory;

        assertTrue(allocatedObjects.ObjectsCount == 0,
           string.Format(AssertTemplates.AssertObjectsCountTemplate, 0, allocatedObjects.ObjectsCount));
        assertTrue(allocatedObjects.SizeInBytes == 0, string.Format(AssertTemplates.AssertExactTotalSizeTemplate, 0, allocatedObjects.SizeInBytes));
      });
    }

    public static void GenericTypeIsTest(Action<bool, string> assertTrue)
    {
      if (dotMemoryApi.IsEnabled)
        dotMemoryApi.CollectAllocations = true;

      var checkpoint1 = dotMemory.Check();
      var garbage = Create<AllocatableTraffic>(AllocatableTraffic.Count);
      if (garbage.Length < AllocatableTraffic.Count)
        throw new InvalidOperationException(); // preventing optimizations

      TypePropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(_ =>
        {
          var allocatedObjects =
            _.GetTrafficFrom(checkpoint1)
            .Where(w => w.Assembly.Is(TypePropertyTestProgram.Generic.Type.Assembly) & w.Type.Is(TypePropertyTestProgram.Generic.Type))
            .AllocatedMemory;

          assertTrue(allocatedObjects.ObjectsCount == TypePropertyTestProgram.Generic.Count,
           string.Format(AssertTemplates.AssertObjectsCountTemplate, TypePropertyTestProgram.Generic.Count, allocatedObjects.ObjectsCount));
          assertTrue(allocatedObjects.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, allocatedObjects.SizeInBytes));
        });
      },
      () => { }
      );
    }

    public static void FailTest(Action<bool, string> assertTrue)
    {
      if (dotMemoryApi.IsEnabled)
        dotMemoryApi.CollectAllocations = true;

      var checkpoint1 = dotMemory.Check();
      var garbage = Create<AllocatableTraffic>(AllocatableTraffic.Count);
      if (garbage.Length < AllocatableTraffic.Count)
        throw new InvalidOperationException();

      dotMemory.Check(_ =>
      {
        var allocatedObjects =
          _.GetTrafficFrom(checkpoint1)
            .Where(w => w.Assembly.Is(typeof(AllocatableTraffic).Assembly))
            .AllocatedMemory;

        assertTrue(allocatedObjects.ObjectsCount <= AllocatableTraffic.Count,
          string.Format(AssertTemplates.AssertObjectsLessOrEqualTemplate, AllocatableTraffic.Count, allocatedObjects.ObjectsCount));
      });
    }
  }

  public class AllocatableTraffic
  {
    public const int Count = 3;
  }
}
