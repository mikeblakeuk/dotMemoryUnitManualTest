using System;

// ReSharper disable once InconsistentNaming
public class TestProgramBase
{
  public static T[] Create<T>(int count) where T : new()
  {
    var array = new T[count];
    for (var i = 0; i < array.Length; i++)
      array[i] = new T();
    return array;
  }

  public static T[] Create<T>(int count, Func<T> factory)
  {
    var array = new T[count];
    for (var i = 0; i < array.Length; i++)
      array[i] = factory();
    return array;
  }

  public static object[] Create<T>(int count, Func<object> factory)
  {
    var array = new object[count];
    for (var i = 0; i < array.Length; i++)
      array[i] = factory();
    return array;
  }
}