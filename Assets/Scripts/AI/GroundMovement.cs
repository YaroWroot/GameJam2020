using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator _animator;
    public Transform _target;
    public float meleeRange = 2f; //Match to current navMeshAgent stopping distance
    public float rotationSpeed = 10f;

    public bool attacking = false;

    public bool stopall = false;

    public float attackDelay = 1f;

    public CharacterAnimation _characterAnimation;

    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        gameObject.name = "Enemy " + Random.Range(0, 9999);
    }

    // Start is called before the first frame update
    private void Start()
    {
        _characterAnimation.SetMovement(true);
    }

    void Update()
    {
        if (stopall)
        {
            _characterAnimation.Attack(3);
            _characterAnimation.SetMovement(false);
        }
        else
        {
            _characterAnimation.SetMovement(true);

            Debug.Log("Distance to player: " + StaticFunctions.DistanceToTarget(transform.position, _target.position));

            if (IsInMeleeRangeOf && !attacking)
            {
                Debug.Log("MeleeRange");
                attacking = true;
                Debug.Log("MeleeAttack");
                _characterAnimation.SetMovement(false);
                agent.enabled = false;
                int rnd = Random.Range(0, 3);
                _characterAnimation.Attack(rnd);
                agent.enabled = true;
                _target.GetComponent<Health>().TakeDamage(Random.Range(2, 10));
                StartCoroutine(MeleeAttack());
                RotateTowards(_target);
            }
            else if (!IsInMeleeRangeOf)
            {
                _characterAnimation.SetMovement(true);
                _characterAnimation.Attack(3);
            }

            agent.SetDestination(_target.transform.position);
        }
    }

    public IEnumerator MeleeAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        
        attacking = false;
    }

    private bool IsInMeleeRangeOf
    {
        get
        {
            return StaticFunctions.DistanceToTarget(transform.position, _target.position) < meleeRange;
        }
    }

    private void RotateTowards(Transform _target)
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

        }
    }

}
