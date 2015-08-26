using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using XUnit;

public class MyTestCollectionOrderer : ITestCollectionOrderer
{
  private readonly IMessageSink diagnosticMessageSink;

  public MyTestCollectionOrderer(IMessageSink diagnosticMessageSink)
  {
    this.diagnosticMessageSink = diagnosticMessageSink;
  }

  public IEnumerable<ITestCollection> OrderTestCollections(IEnumerable<ITestCollection> testCollections)
  {
    var result = testCollections.ToList();
    return result;
  }
}

public class MyTestCaseOrderer : ITestCaseOrderer
{
  private readonly IMessageSink diagnosticMessageSink;

  public MyTestCaseOrderer(IMessageSink diagnosticMessageSink)
  {
    this.diagnosticMessageSink = diagnosticMessageSink;
  }

  public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
      where TTestCase : ITestCase
  {
    var result = testCases.ToList();

    /*if (result.Any(_ => _.DisplayName.Equals(typeof(MainTestClass).FullName + ".BeforeAllTest")) == true)
    {
      var firstTest = result.First(_ => _.DisplayName.Equals(typeof(MainTestClass).FullName + ".BeforeAllTest"));
      result.Remove(firstTest);
      result.Insert(0, firstTest);
      var e = 0;
    }*/

    return result;
  }
}