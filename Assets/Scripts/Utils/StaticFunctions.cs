using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticFunctions
{
    public static float DistanceToTarget(Vector3 start, Vector3 target)
    {
        return Vector3.Distance((start - new Vector3(0, start.y, 0)), target);
    }
    public static Transform GetClosestEnemy(Transform player, Transform[] enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = player.position;
        foreach (Transform t in enemies)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

}
