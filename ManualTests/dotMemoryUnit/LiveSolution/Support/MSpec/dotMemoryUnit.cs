using System;
using JetBrains.dotMemoryUnit.Kernel;

namespace JetBrains.dotMemoryUnit
{
  /// <summary>
  /// This class used for using with MSpec testing framework, but of cource could be used with any other
  /// </summary>
  /// <remarks>
  /// Also it demonstrates a principlte how to write your own framework if embeeded into dotMemory Unit is not suitable for some reason
  /// See <see cref="QueryBuilder"/> before writing your own framework, it's possible that it is enough to meet your needs
  /// </remarks>
  public static class dotMemoryUnit
  {
    /// <summary>
    /// Collects data from the point when <see cref="CollectDataOnInterval"/> is called till returned object is disposed
    /// This call create test scope automatically, there is no need to call <see cref="dotMemoryUnit.TestScope"/> when use <see cref="CollectDataOnInterval"/>
    /// </summary>
    /// <param name="collectAllocations">Set collectAllocations to true in order to obtain information about memory traffic</param>
    /// <returns>Instance of <see cref="TimeInterval"/> type used to get memory data</returns>
    public static TimeInterval CollectDataOnInterval(bool collectAllocations)
    {
      DotMemoryUnitController.TestStart();
      return new TimeInterval(collectAllocations);
    }

    /// <summary>
    /// Wrap code contains <see cref="dotMemoryUnit"/> calls with using(dotMemoryUnit.TestScope())
    /// </summary>
    /// <param name="collectAllocations">Set collectAllocations to true in order to obtain information about memory traffic</param>
    public static IDisposable TestScope(bool collectAllocations = false)
    {
      return new Controller(collectAllocations);
    }

    /// <summary>
    /// Checks whether a test is run under dotMemory Unit.
    /// Use it before calling the assert statements if test is run under dotMemory Unit and as usual unit tests
    /// </summary>
    public static bool IsEnabled
    {
      get { return dotMemoryApi.IsEnabled; }
    }

    public static bool CollectAllocations
    {
      get { return dotMemoryApi.CollectAllocations; }
      set { dotMemoryApi.CollectAllocations = value; }
    }

    /// <summary>
    /// Gets a memory snapshot. See the <see cref="M:JetBrains.dotMemoryUnit.FluentQueries.GetObjects(JetBrains.dotMemoryUnit.Sugar.ObjectSet,System.Func{JetBrains.dotMemoryUnit.Properties.ObjectProperty,JetBrains.dotMemoryUnit.Client.Interface.Query})"/> extension method for details.
    /// </summary>
    public static Snapshot GetSnapshot()
    {
      return dotMemoryApi.GetSnapshot();
    }

    /// <summary>
    /// Saves the workspace containing all memory snapshots collected during test scope to the specified directory. <see cref="dotMemoryUnit.TestScope"/>
    /// To view the collected snapshots, open the workspace in the standalone dotMemory profiler using the File | Import Workspace menu.
    /// </summary>
    /// <param name="directoryPath">Path to the directory where workspace files must be saved. This can be either a full path or a path relative to the profiled assembly where test method is declared.</param>
    public static void SaveCollectedData(string directoryPath = null)
    {
      dotMemoryApi.SaveCollectedData(directoryPath);
    }

    /// <summary>
    /// Represents time interval on which memory data was collected
    /// </summary>
    public class TimeInterval : IDisposable
    {
      private readonly Snapshot snapshot1;
      private Snapshot snapshot2;

      public TimeInterval(bool collectAllocations)
      {
        dotMemoryApi.CollectAllocations = collectAllocations;
        snapshot1 = dotMemoryApi.GetSnapshot();
      }

      /// <summary>
      /// Memory traffic data from the point when <see cref="CollectDataOnInterval"/> is called till returned object is disposed
      /// </summary>
      public Traffic MemoryTraffic
      {
        get
        {
          if (snapshot2 == null)
            throw new InvalidOperationException("Do not use inside TestScope, use it for assertions only");
          return dotMemoryApi.GetTrafficBetween(snapshot1, snapshot2);
        }
      }

      /// <summary>
      /// Difference between two memory snapshots the first snapshot of the point when <see cref="CollectDataOnInterval"/> is called and the second snapshot of the point where returned object is disposed
      /// </summary>
      public SnapshotDifference MemoryDifference
      {
        get
        {
          if (snapshot2 == null) throw new InvalidOperationException("Do not use inside using");
          return dotMemoryApi.GetDifference(snapshot1, snapshot2);
        }
      }

      void IDisposable.Dispose()
      {
        snapshot2 = dotMemoryApi.GetSnapshot();
        dotMemoryApi.CollectAllocations = false;
        DotMemoryUnitController.TestEnd();
      }
    }

    private struct Controller : IDisposable
    {
      public Controller(bool collectAllocations)
      {
        DotMemoryUnitController.TestStart();
        dotMemoryApi.CollectAllocations = collectAllocations;
      }

      public void Dispose()
      {
        dotMemoryApi.CollectAllocations = false;
        DotMemoryUnitController.TestEnd();
      }
    }
  }
}