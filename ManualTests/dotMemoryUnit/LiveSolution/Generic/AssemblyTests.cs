using System;
using System.Collections.Generic;
using JetBrains.dotMemoryUnit;

namespace Generic
{
  public static class AssemblyTests
  {
    private static IEnumerable<string> GetLikePatterns()
    {
      var fullName = typeof(AssemblyPropertyTestProgram).Assembly.FullName;
      yield return fullName;
      yield return fullName.Substring(0, 10) + "*";
      yield return "*" + fullName.Substring(10, fullName.Length - 10);

      var shortName = fullName.Substring(0, fullName.IndexOf(",", StringComparison.Ordinal));
      yield return shortName + "*";
      yield return shortName.Substring(0, 5) + "*";
      yield return "*" + shortName.Substring(5, shortName.Length - 5) + "*";
    }

    public static void LikeTest(Action<bool, string> assertTrue)
    {
      AssemblyPropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var patterns = GetLikePatterns();
          foreach (var pattern in patterns)
          {
            Console.WriteLine(pattern);
            var objectSet = memory.GetObjects(_ => _.Assembly.Like(pattern));
            assertTrue(objectSet.ObjectsCount == AssemblyPropertyTestProgram.Local.Count + 1, // + 1 is Local[]
              string.Format(AssertTemplates.AssertObjectsCountTemplate, AssemblyPropertyTestProgram.Local.Count + 1, objectSet.ObjectsCount));
            assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
          }
        });
      });
    }

    public static void IsTest(Action<bool, string> assertTrue)
    {
      AssemblyPropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var objectSet = memory.GetObjects(_ => _.Assembly.Is(typeof(AssemblyPropertyTestProgram).Assembly));
          assertTrue(objectSet.ObjectsCount == AssemblyPropertyTestProgram.Local.Count + 1,
           string.Format(AssertTemplates.AssertObjectsCountTemplate, AssemblyPropertyTestProgram.Local.Count + 1, objectSet.ObjectsCount));
          assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
        });
      });
    }

    public static void NotLikeTest(Action<bool, string> assertTrue)
    {
      AssemblyPropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var patterns = GetLikePatterns();
          var totalObjects = memory.ObjectsCount;
          foreach (var pattern in patterns)
          {
            Console.WriteLine(pattern);
            var objectSet = memory.GetObjects(_ => _.Assembly.NotLike(pattern));
            assertTrue(objectSet.ObjectsCount == totalObjects - AssemblyPropertyTestProgram.Local.Count - 1,
              string.Format(AssertTemplates.AssertObjectsCountTemplate, totalObjects - AssemblyPropertyTestProgram.Local.Count - 1, objectSet.ObjectsCount));
            assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
          }
        });
      });
    }

    public static void IsNotTest(Action<bool, string> assertTrue)
    {
      AssemblyPropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var totalObjects = memory.ObjectsCount;
          var objectSet = memory.GetObjects(_ => _.Assembly.IsNot(typeof(AssemblyPropertyTestProgram).Assembly));
          assertTrue(objectSet.ObjectsCount == totalObjects - AssemblyPropertyTestProgram.Local.Count - 1,
           string.Format(AssertTemplates.AssertObjectsCountTemplate, totalObjects - AssemblyPropertyTestProgram.Local.Count - 1, objectSet.ObjectsCount));
          assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
        });
      });
    }

    public static void IsNotByExclusionTest(Action<bool, string> assertTrue)
    {
      AssemblyPropertyTestProgram.Execute(() =>
      {
        var mscorlibAssembly = typeof(int).Assembly;
        dotMemory.Check(memory =>
        {
          var isObjectSet = memory.GetObjects(_ => _.Assembly.Is(mscorlibAssembly));
          var isNotObjectSet = memory.GetObjects(_ => _.Assembly.IsNot(mscorlibAssembly));
          assertTrue(isNotObjectSet.ObjectsCount == memory.ObjectsCount - isObjectSet.ObjectsCount,
            string.Format(AssertTemplates.AssertObjectsCountTemplate, memory.ObjectsCount - isObjectSet.ObjectsCount, isNotObjectSet.ObjectsCount));
          assertTrue(isNotObjectSet.SizeInBytes == memory.SizeInBytes - isObjectSet.SizeInBytes,
            string.Format(AssertTemplates.AssertExactTotalSizeTemplate, memory.SizeInBytes - isObjectSet.SizeInBytes, isNotObjectSet.SizeInBytes));
        });
      });
    }

    public static void ArrayTest(Action<bool, string> assertTrue)
    {
      AssemblyPropertyTestProgram.Execute(() =>
      {
        var mscorlibAssembly = typeof(int).Assembly;
        dotMemory.Check(memory =>
        {
          var objectSet = memory.GetObjects(_ => _.Assembly.Is(typeof(AssemblyPropertyTestProgram.Local[]).Assembly) & _.Type.Is<AssemblyPropertyTestProgram.Local[]>());
          assertTrue(objectSet.ObjectsCount == 1,
           string.Format(AssertTemplates.AssertObjectsCountTemplate, 1, objectSet.ObjectsCount));
          assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
        });
      });
    }

    public static void ArrayNotInMscorlibTest(Action<bool, string> assertTrue)
    {
      AssemblyPropertyTestProgram.Execute(() =>
      {
        var mscorlibAssembly = typeof(int).Assembly;
        dotMemory.Check(memory =>
        {
          var objectSet = memory.GetObjects(_ => _.Assembly.Is(mscorlibAssembly) & _.Type.Is<AssemblyPropertyTestProgram.Local[]>());
          assertTrue(objectSet.ObjectsCount == 0,
           string.Format(AssertTemplates.AssertObjectsCountTemplate, 0, objectSet.ObjectsCount));
          assertTrue(objectSet.SizeInBytes == 0, string.Format(AssertTemplates.AssertExactTotalSizeTemplate, 0, objectSet.SizeInBytes));
        });
      });
    }

    public static void EmptyIntersectionTest(Action<bool, string> assertTrue)
    {
      AssemblyPropertyTestProgram.Execute(() =>
      {
        var mscorlibAssembly = typeof(int).Assembly;
        dotMemory.Check(memory =>
        {
          var objectSet = memory.GetObjects(_ => _.Assembly.Is(mscorlibAssembly) & _.Type.Is<AssemblyPropertyTestProgram.Local>());
          assertTrue(objectSet.ObjectsCount == 0,
           string.Format(AssertTemplates.AssertObjectsCountTemplate, 0, objectSet.ObjectsCount));
          assertTrue(objectSet.SizeInBytes == 0, string.Format(AssertTemplates.AssertExactTotalSizeTemplate, 0, objectSet.SizeInBytes));
        });
      });
    }

    public static void GenericTypeIstest(Action<bool, string> assertTrue)
    {
      TypePropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var objectSet = memory.GetObjects(_ => _.Assembly.Is(TypePropertyTestProgram.Generic.Type.Assembly) & _.Type.Is(TypePropertyTestProgram.Generic.Type));
          assertTrue(objectSet.ObjectsCount == TypePropertyTestProgram.Generic.Count,
           string.Format(AssertTemplates.AssertObjectsCountTemplate, TypePropertyTestProgram.Generic.Count, objectSet.ObjectsCount));
          assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
        });
      });
    }
  }
}
