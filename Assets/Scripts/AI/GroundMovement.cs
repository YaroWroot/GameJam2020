using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundMovement : MonoBehaviour
{
    private GameObject target;
    public NavMeshAgent agent;

    public CharacterAnimation _characterAnimation;

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
        if(StaticFunctions.DistanceToTarget(transform.position, target.transform.position) > 0.5f)
        {
            _characterAnimation.SetMovement(true);
        }
        else
        {
            _characterAnimation.SetMovement(false);
        }        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

        }
    }

}
