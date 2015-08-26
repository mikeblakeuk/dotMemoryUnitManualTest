using System;
using JetBrains.dotMemoryUnit;
using JetBrains.dotMemoryUnit.Properties;

namespace Generic
{
  public static class ObjectSetByGenerationTests
  {
    public static void LohTest(Action<bool, string> isTrue)
    {
      GenerationPropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var objectSet = memory
            .GetObjects(_ => _.Generation.Is(Generation.LOH))
            .GetObjects(_ => _.Type.Is(GenerationPropertyTestProgram.Loh.Type));

          isTrue(GenerationPropertyTestProgram.Loh.Count == objectSet.ObjectsCount,
            string.Format(AssertTemplates.AssertObjectsCountTemplate, GenerationPropertyTestProgram.Loh.Count, objectSet.ObjectsCount));
          isTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
        });
      }, () => { }, () => { });
    }

    public static void Gen0Test(Action<bool, string> isTrue)
    {
      GenerationPropertyTestProgram.Execute(() =>
      {
        dotMemory.Check(memory =>
        {
          var objectSet = memory
            .GetObjects(where => where.Generation.Is(Generation.Gen1, Generation.Gen2, Generation.LOH))
            .GetObjects(where => where.Type.Is<GenerationPropertyTestProgram.Gen1>()
                           | where.Type.Is<GenerationPropertyTestProgram.Gen2>());

          isTrue(0 == objectSet.ObjectsCount,
            string.Format(AssertTemplates.AssertObjectsCountTemplate, 0, objectSet.ObjectsCount));
          isTrue(objectSet.SizeInBytes == 0, string.Format(AssertTemplates.AssertExactTotalSizeTemplate, 0, objectSet.SizeInBytes));
        });
      }, () => { }, () => { });
    }

    public static void Gen1Test(Action<bool, string> isTrue)
    {
      GenerationPropertyTestProgram.Execute(
        () => { dotMemory.Check(); },
        () =>
        {
          dotMemory.Check(memory =>
          {
            var objectSet = memory
              .GetObjects(where => where.Generation.Is(Generation.Gen1))
              .GetObjects(where => where.Type.Is<GenerationPropertyTestProgram.Gen1>());

            isTrue(GenerationPropertyTestProgram.Gen1.Count == objectSet.ObjectsCount,
              string.Format(AssertTemplates.AssertObjectsCountTemplate, GenerationPropertyTestProgram.Gen1.Count,
                objectSet.ObjectsCount));
            isTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
          });
        },
        () => { });
    }

    public static void Gen2Test(Action<bool, string> isTrue)
    {
      GenerationPropertyTestProgram.Execute(
        () => { dotMemory.Check(); },
        () => { dotMemory.Check(); },
        () =>
        {
          dotMemory.Check(memory =>
          {
            var objectSet = memory
              .GetObjects(where => where.Generation.Is(Generation.Gen2))
              .GetObjects(where => where.Type.Is<GenerationPropertyTestProgram.Gen1>()
                           | where.Type.Is<GenerationPropertyTestProgram.Gen2>());

            const int expectedCount = GenerationPropertyTestProgram.Gen1.Count + GenerationPropertyTestProgram.Gen2.Count;
            isTrue(expectedCount == objectSet.ObjectsCount,
              string.Format(AssertTemplates.AssertObjectsCountTemplate, expectedCount, objectSet.ObjectsCount));
            isTrue(objectSet.SizeInBytes > 0, string.Format(AssertTemplates.AssertSizeInBytesTemplate, objectSet.SizeInBytes));
          });
        });
    }
  }
}