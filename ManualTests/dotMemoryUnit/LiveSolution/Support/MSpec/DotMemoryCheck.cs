using System;
using System.IO;
using System.Runtime.CompilerServices;
using JetBrains.dotMemoryUnit.Kernel;

namespace JetBrains.dotMemoryUnit.MSpec
{
  public static class DotMemoryCheck
  {
    public const int BytesInKb = 1024;

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static long Test(Action action, long? saveIfOverKb = null)
    {
      var start = Start();
      action.Invoke();
      var size = End(start, saveIfOverKb) / BytesInKb;
      return size;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Snapshot Start()
    {
      DotMemoryUnitController.TestStart();
      return dotMemoryApi.GetSnapshot();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static long End(Snapshot start, long? saveIfOverKb = null)
    {
      var memoryCheckPoint2 = dotMemoryApi.GetSnapshot();

      var diff = dotMemoryApi.GetDifference(start, memoryCheckPoint2);
      var result = diff.GetNewObjects().SizeInBytes - diff.GetDeadObjects().SizeInBytes;

      if (saveIfOverKb.HasValue && result > saveIfOverKb * BytesInKb)
      {
        //var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Snapshots");
        Console.WriteLine($"Saving collected data because {result / BytesInKb}k is bigger than {saveIfOverKb}k");
        dotMemoryApi.SaveCollectedData();
      }
      DotMemoryUnitController.TestEnd();
      return result;
    }
  }
}
