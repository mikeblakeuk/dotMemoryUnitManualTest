using System;
using System.Diagnostics;
#if PROFILE
using JetBrains.Profiler.Windows.Api;
#endif

public static class ProfilingApi
{
  public const string Filename = "ProfilingApi.cs";
  public const string ProfileDefine = "PROFILE";

  [Conditional(ProfileDefine)]
  public static void AssertProfilerIsConnected()
  {
#if PROFILE
    if( !MemoryProfiler.IsActive )
		  throw new InvalidOperationException("Is not under profiling");
#endif
  }

  [Conditional(ProfileDefine)]
  public static void GetSnapshot()
  {
#if PROFILE
    AssertProfilerIsConnected();
    MemoryProfiler.Dump();
#endif
  }

  [Conditional(ProfileDefine)]
  public static void EnableAllocations()
  {
#if PROFILE
    AssertProfilerIsConnected();
    if(!MemoryProfiler.CanControlAllocations)
      throw new InvalidOperationException();
    MemoryProfiler.EnableAllocations();
#endif
  }

  public static void DisableAllocations()
  {
#if PROFILE
    AssertProfilerIsConnected();
    if (!MemoryProfiler.CanControlAllocations)
      throw new InvalidOperationException();
    MemoryProfiler.DisableAllocations();
#endif
  }
}