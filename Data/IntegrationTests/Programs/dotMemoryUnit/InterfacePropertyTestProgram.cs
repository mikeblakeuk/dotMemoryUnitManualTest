using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace JetBrains.dotMemoryUnit.TestPrograms
{
  public class InterfacePropertyTestProgram : TestProgramBase
  {
    public const int Live = 3;
    public const int Collected = 2;

    public static void Main(string[] args)
    {
      ProfilingApi.AssertProfilerIsConnected();
      ProfilingApi.EnableAllocations();

      Execute(() => ProfilingApi.GetSnapshot(), () => ProfilingApi.GetSnapshot());
    }

    public static void Execute(Action getSnapshot0, Action getSnapshot1)
    {
      var keepAlive = new List<object>(CreateObjects(1)){CreateNoise()};

      getSnapshot0();

      // for traffic test
      keepAlive.AddRange(
        CreateObjects(Live));
      CreateObjects(Collected);

      getSnapshot1();

      GC.KeepAlive(keepAlive);
    }

    private static object[] CreateObjects(int multiplicator)
    {
      return new object[]
      {
        Create<C12>(C12.Count * multiplicator),
        Create<C2>(C2.Count * multiplicator),
        Create<GenericInterfaceImpl>(GenericInterfaceImpl.Count * multiplicator),
        Create(GenericClass.Count * multiplicator, () => Activator.CreateInstance(GenericClass.ObjectType)),
        Create<DictionaryTest>(DictionaryTest.Count * multiplicator),
        Create<KCollection>(KCollection.Count * multiplicator),
        Create<ArrayGenericArgument>(ArrayGenericArgument.Count * multiplicator),
        Create<DeepClassInheritance>(DeepClassInheritance.Count * multiplicator)
      };
    }

    private static object CreateNoise()
    {
      CreateObjects(1);

      return new object[] {new Dictionary<string, int>(), new KByTypeCollection<IDisposable>(), 
        new ArrayGenericArgument<object>(), new List<int[][,]>()};
    }

    public interface I1 { }
    public interface I2 { }

    public class C12 : I1, I2
    {
      public const int Count = 32;
    }

    public class C2 : I2
    {
      public const int Count = 6;
    }

    public interface IGenericInterface1<T1>{}
    public interface IGenericInterface2<T1, T2>{}
    public class OpenGenericBase2<B1, B2> : IGenericInterface2<int, B1> { }
    public class OpenGeneric2<T1, T2> : OpenGenericBase2<T2, T1>{}

    public class GenericClass
    {
      public const int Count = 7;
      public static readonly Type ObjectType = typeof (OpenGeneric2<List<IEnumerable<long>>, List<IEnumerable<bool>>>);
      public static readonly Type InterfaceType;

      static GenericClass()
      {
        InterfaceType = ObjectType.GetInterfaces()[0];
      }
    }

    public class GenericInterfaceImpl : OpenGeneric2<string, List<int>>, IGenericInterface1<GenericInterfaceImpl>
    {
      public const int Count = 19;

      public static readonly Type InterfaceType1;
      public static readonly Type InterfaceType2;

      static GenericInterfaceImpl()
      {
        InterfaceType1 = typeof(GenericInterfaceImpl).GetInterfaces()[0];
        InterfaceType2 = typeof(GenericInterfaceImpl).GetInterfaces()[1];
      }
    }

    public class DictionaryTest
    {
      public const int Count = 49;
      public static readonly Type Type = typeof(Dictionary<IGenericInterface1<long>, OpenGeneric2<bool, string>>);
      private readonly object myObject = Activator.CreateInstance(Type);
    }

    public class KCollection<TKey, TItem> : Collection<TItem>
    { }

    private class KByTypeCollection<TItem> : KCollection<Type, TItem>
    { }

    public class KCollection
    {
      public const int Count = 17;
      public static readonly Type Type = typeof(KByTypeCollection<IList<List<int>>>);
      private readonly object myObject = Activator.CreateInstance(Type);
    }

    private class ArrayGenericArgument<TItem> : Collection<TItem[,][]>
    {}

    public class ArrayGenericArgument
    {
      public const int Count = 13;
      public static readonly Type Type = typeof(ArrayGenericArgument<string[][,,]>);
      private readonly object myObject = Activator.CreateInstance(Type);
    }

    public interface IDeepClassInheritance<T> { }
    public class Base1<T> : IDeepClassInheritance<T>{ }
    public class Base2<T> : Base1<T> { }

    public class DeepClassInheritance : Base2<string[]>
    {
      public const int Count = 17;
      public static readonly Type InterfaceType = typeof(DeepClassInheritance).GetInterfaces()[0];
    }

    public interface IAbsent { }
  }
}