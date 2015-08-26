using System;
using JetBrains.dotMemoryUnit;
using NamespaceTest_LevelOne.LevelTwo;
using NamespaceTest_SiblingOne;

namespace Generic
{
  public static class NamespaceTests
  {
    public static void LikeTest(Action<bool, string> assertTrue)
    {
      NamespacePropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var objectSet = memory.GetObjects(_ => _.Namespace.Like(typeof(Two).Namespace, typeof(Sibling).Namespace));
          assertTrue(objectSet.ObjectsCount == Two.Count + Sibling.Count + 2, //+2 means One[] and Sibling[] arrays
            string.Format(AssertTemplates.AssertObjectsCountTemplate, Two.Count + Sibling.Count + 2, objectSet.ObjectsCount));

          assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
        });
      });
    }

    public static void NotLikeTest(Action<bool, string> assertTrue)
    {
      NamespacePropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var totalObjects = memory.ObjectsCount;
          var objectSet = memory.GetObjects(_ => _.Namespace.NotLike(typeof(Two).Namespace, typeof(Sibling).Namespace));

          assertTrue(objectSet.ObjectsCount == totalObjects - (Two.Count + Sibling.Count + 2 /*+2 means One[] and Sibling[] arrays*/),
            string.Format(AssertTemplates.AssertObjectsCountTemplate, totalObjects - (Two.Count + Sibling.Count + 2), objectSet.ObjectsCount));

          assertTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
        });
      });
    }
  }
}