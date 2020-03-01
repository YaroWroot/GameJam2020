using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundMovement : MonoBehaviour
{
    private GameObject target;
    public NavMeshAgent agent;
    public Animator _animator;
    public Transform _target;
    public float meleeRange = 4f; //Match to current navMeshAgent stopping distance
    public float rotationSpeed = 10f;
    private bool _stopMovement = false;

    public CharacterAnimation _characterAnimation;

    // Start is called before the first frame update
    private void Start()
    {
        SetTarget();
        _characterAnimation.SetMovement(true);
    }

    void Update()
    {
        _characterAnimation.SetMovement(true);
        SetTarget();

        //Will call attack but when enemy spawns from waves this way, animations will break unline other method.

        //if (IsInMeleeRangeOf(_target))
        //{
        //    StartCoroutine(MeleeAttack());
        //    _characterAnimation.SetMovement(false);
        //    RotateTowards(_target);
        //}
        //else
        //{
        //    _characterAnimation.SetMovement(true);
        //}
    }

    IEnumerator MeleeAttack()
    {
        yield return new WaitForSeconds(2f);
        int rnd = Random.Range(0, 3);
        _characterAnimation.Attack(rnd);
        _target.GetComponent<Health>().TakeDamage(Random.Range(2, 10));
    }

    private bool IsInMeleeRangeOf(Transform _target)
    {
        float distance = Vector3.Distance(transform.position, _target.position);
        return distance < meleeRange;
    }

    private void RotateTowards(Transform _target)
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
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
        if (StaticFunctions.DistanceToTarget(transform.position, target.transform.position) > 4f)
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
