using JetBrains.dotMemoryUnit;
using Xunit;
using Xunit.Abstractions;

[assembly: EnableDotMemoryUnitSupport]
[assembly: DotMemoryUnit(FailIfRunWithoutSupport = false, SavingStrategy = SavingStrategy.OnAnyFail, Directory = @"c:\tmp\Assembly")]
[assembly: CollectionBehavior(DisableTestParallelization = true)]
//[assembly: TestCollectionOrderer("MyTestCollectionOrderer", "xUnit")]
//[assembly: TestCaseOrderer("MyTestCaseOrderer", "xUnit")]
//Try to order tests (there isn't ready solution for running a special test first, 
//because we need to create class constructor with parameter (to call DotMemoryUnitTestOutput.SetOutputMethod),
//but ICollectionFixture require class with non-param constructor)
//Now we should call DotMemoryUnitTestOutput.SetOutputMethod for each class
/*
namespace XUnit
{
  public class BeforeAllTestsRun 
  {
    public BeforeAllTestsRun()
    {
      
      
    }

    public void SetMethod(ITestOutputHelper atr)
    {
      DotMemoryUnitTestOutput.SetOutputMethod(atr.WriteLine);
    }
  }

  [CollectionDefinition("BeforeAllTestsRun")]
  public class BeforeAllTestsRunCollection : ICollectionFixture<BeforeAllTestsRun>
  {
    public BeforeAllTestsRunCollection(ITestOutputHelper atr)
    {
      DotMemoryUnitTestOutput.SetOutputMethod(atr.WriteLine);   
    }
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
  }

  public class MainTestClass
  {
    public MainTestClass(ITestOutputHelper atr)
    {
      DotMemoryUnitTestOutput.SetOutputMethod(atr.WriteLine);
    }

    [Fact]
    public void BeforeAllTest()
    {

    }
  }
}*/
