using UnityEngine;

public static class MathFunctions
{

    public static float EaseOutExpo(float x)
    {

        return x >= 1f ? 1f : 1f - Mathf.Pow(2f, -10f * x);

    }
}