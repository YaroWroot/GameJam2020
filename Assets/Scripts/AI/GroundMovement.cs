using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundMovement : MonoBehaviour
{
    private GameObject target;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        SetTarget();
    }

    void Update()
    {
        SetTarget();
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            SetTarget();
        }
    }

    private void SetTarget()
    {
        var target = GameObject.FindGameObjectWithTag("Player");
        agent.SetDestination(target.transform.position);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

        }
    }

}
