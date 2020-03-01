using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float meleeRange = 2f; //Match to current navMeshAgent stopping distance
    public float rotationSpeed = 10f;
    Vector3[] path;
    int targetIndex;

    private void Start()
    {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    private void Update()
    {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        if (IsInMeleeRangeOf(target))
        {
            RotateTowards(target);
        }
    }
    private bool IsInMeleeRangeOf(Transform target)
    {
        float distance = Vector3.Distance(transform.position, target.position);
        return distance < meleeRange;
    }

    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccesful)
    {
        if (pathSuccesful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];

        while (true)
        {
            if(transform.position == currentWaypoint)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed);
            yield return null;
        }
    }
}
