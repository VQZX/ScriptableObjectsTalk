using UnityEngine;
using System.Collections;

public static class Helper2D
{
    public static Vector3 Rotate2D(UnityEngine.Transform transform, float angle)
    {
        Vector3 euler = transform.eulerAngles;
        euler.z = angle;
        transform.eulerAngles = euler;
        return euler;
    }
}
