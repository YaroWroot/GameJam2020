using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticFunctions
{
    public static float DistanceToTarget(Vector3 start, Vector3 target)
    {
        return Vector3.Distance((start - new Vector3(0, start.y, 0)), target);
    }
}
