using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.dotMemoryUnit.MSpec;
using Machine.Specifications;

namespace MSpec
{
  [Subject(typeof(DotMemoryCheck))]
  public class DotMemoryCheckTests
  {
    public class when_nothing_happens
    {
      Because of_memory_test = () => _memoryResult = DotMemoryCheck.Test(UniqueStrings);

      static void UniqueStrings()
      {
        _paths = new List<string>();
        GC.KeepAlive(_paths);
      }

      It should_load_strings = () => _paths.Count.ShouldEqual(0);

      It should_have_manageable_memory_foot_print = () => _memoryResult.ShouldBeLessThan(1024);

      static List<string> _paths;
      static long _memoryResult;
    }

    public class when_loading_unique_strings
    {
      const long ExpectedKb = 61;

      Because of_memory_test = () =>
      {
        var start = DotMemoryCheck.Start();
        UniqueStrings();
        _memoryResult = DotMemoryCheck.End(start, ExpectedKb);
      };

      static void UniqueStrings()
      {
        _paths = new List<string>();
        for (var i = 0; i < 1000; i++)
        {
          _paths.Add("UniqueString" + i % 10);
        }

        GC.KeepAlive(_paths);
      }

      It should_load_strings = () => _paths.Count.ShouldEqual(1000);

      It should_have_manageable_memory_foot_print = () => _memoryResult.ShouldBeLessThan(ExpectedKb * 1024);

      static List<string> _paths;
      static long _memoryResult;
    }

    public class when_loading_intern_strings
    {
      const long ExpectedKb = 10;
      Because of_memory_test = () =>
      {
        var start = DotMemoryCheck.Start();
        UniqueStrings();
        _memoryResult = DotMemoryCheck.End(start, ExpectedKb);
      };

      static void UniqueStrings()
      {
        _paths = new List<string>();
        for (var i = 0; i < 1000; i++)
        {
          _paths.Add(string.Intern("InternString" + i % 10));
        }

        GC.KeepAlive(_paths);
      }

      It should_load_strings = () => _paths.Count.ShouldEqual(1000);

      It should_have_manageable_memory_foot_print = () => _memoryResult.ShouldBeLessThan(ExpectedKb * DotMemoryCheck.BytesInKb);

      static List<string> _paths;
      static long _memoryResult;
    }

    [Ignore("Example failing memory test to show snapshots work")]
    public class when_failing
    {
      const long ExpectedKb = 1;
      Because of_memory_test = () =>
      {
        var start = DotMemoryCheck.Start();
        UniqueStrings();
        _memoryResult = DotMemoryCheck.End(start, ExpectedKb);
      };

      static void UniqueStrings()
      {
        _paths = new List<string>();
        for (var i = 0; i < 1000; i++)
        {
          _paths.Add(string.Intern("InternString" + i % 10));
        }

        GC.KeepAlive(_paths);
      }

      //in TeamCity it could be temp
      It should_save_a_snapshot = () => Directory.EnumerateFiles(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Temp\dotMemoryUnitWorkspace"), "Start.*.dmw", SearchOption.AllDirectories).Count().ShouldBeGreaterThan(0);

      It should_fail = () => _memoryResult.ShouldBeLessThan(ExpectedKb * DotMemoryCheck.BytesInKb);

      static List<string> _paths;
      static long _memoryResult;
    }
  }
}
