using System;
using System.Collections.Generic;


public static class Tools {

  public static void Shuffle<T>(List<T> list, Random rnd)
  {
      for(var i=0; i < list.Count; i++)
          Swap(list, i, rnd.Next(i, list.Count));
  }

  public static void Swap<T>(List<T> list, int i, int j)
  {
      var temp = list[i];
      list[i] = list[j];
      list[j] = temp;
  }
}
