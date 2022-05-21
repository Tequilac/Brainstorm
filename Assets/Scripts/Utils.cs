using UnityEngine;
 
 public static class Utils
 {
    public static int RoundToNearestMultiple(float value, int multiple)
    {
        return (int)Mathf.Round(value / multiple) * multiple;
    }
 }