using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _speed = 10.0f;
    public LayerMask _ignoreClickLayer;
    public LayerMask _ignorePlayer;
    public GameObject _playerAsset;
    public CharacterController _characterController;
    public Collider _intersectCheck;
    public Animator _animator;

    private GameObject _interactableMovingTo = null;
    private Transform _movePointLocation;
    private Vector3 _moveToPoint;
    private bool _stopMovement = false;
    private PlayerWeapons _playerWeapons;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _animator.enabled = true;

        _playerWeapons = GetComponentInChildren<PlayerWeapons>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _stopMovement = false;
            var target = LeftMouseHit();

            if(target.tag == "Enemy")
            {
                Debug.Log("Hit Enemy");
                if (_intersectCheck.bounds.Intersects(target.transform.GetComponent<Collider>().bounds))
                {
                    _animator.SetTrigger("Attack");

                    if (_interactableMovingTo != null) _interactableMovingTo = null;
                }
                else
                {
                    _interactableMovingTo = target;
                    _moveToPoint = target.transform.position;               
                }
            }
            else if(target.tag == "Interactable")
            {
                if (_intersectCheck.bounds.Intersects(target.transform.GetComponent<Collider>().bounds))
                {
                    target.GetComponent<IInteractable>().Interact(this);

                    if (_interactableMovingTo != null) _interactableMovingTo = null;
                }
                else
                {
                    _interactableMovingTo = target;
                    _moveToPoint = target.transform.position;
                    
                }
            }
        }
        if(Input.GetMouseButton(1))
        {
            _stopMovement = false;
            _interactableMovingTo = null;
            LocateHit();
        }
        if (Input.GetKey(KeyCode.Q))
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            List<Transform> enemiesInRange = new List<Transform>();
            var count = enemies.Length;

            foreach (var e in enemies)
            {
                enemiesInRange.Add(e.transform);
            }


            var nearestEnemy = StaticFunctions.GetClosestEnemy(transform, enemiesInRange.ToArray());
            LookToClick(nearestEnemy.position, 100);
            _playerWeapons.FireBow(_animator);
        }

        MoveCharacter();
        CheckIfStoredInteractIsInRange();
        MoveLocSpriteSpawn();
        DeleteMovePointLocation();
    }

    private void CheckIfStoredInteractIsInRange()
    {
        if (_interactableMovingTo == null) return;

        if (_intersectCheck.bounds.Intersects(_interactableMovingTo.transform.GetComponent<Collider>().bounds))
        {
            _stopMovement = true;
            Debug.Log("CheckIfStoredInteractIsInRange");

            if (_interactableMovingTo.tag == "Enemy")
            {
                
                Debug.Log("Hit Enemy");
                _animator.SetTrigger("Attack");
            }
            else
            {
                Debug.Log("Interact");
                _interactableMovingTo.GetComponent<IInteractable>().Interact(this);
            }

            if (_interactableMovingTo != null) _interactableMovingTo = null;

            //_moveToPoint = transform.position;
            //_stopMovement = false;
        }
    }

    private GameObject LeftMouseHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        GameObject retVal = null;

        if (Physics.Raycast(ray, out hit, 500, ~_ignorePlayer) && hit.collider.tag != "Obstacle")
        {
            retVal = hit.collider.gameObject;
            Debug.Log(hit.collider.tag);
        }

        return retVal;
    }

    private void LocateHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 500, ~_ignorePlayer) && hit.collider.tag != "Obstacle")
        {            
            _moveToPoint = hit.point;
        }
    }

    private void MoveLocSpriteSpawn()
    {
        if (_movePointLocation == null)
        {
            _movePointLocation = Instantiate(GameAssets.i.MovePointIndicator, _moveToPoint, Quaternion.identity);
        }
        else
        {
            _movePointLocation.position = _moveToPoint;
        }
    }

    private void LookToClick(Vector3 target, float multiplier)
    {
        Quaternion lookAtRot = Quaternion.LookRotation(target - transform.position, Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation,
                                              new Quaternion(0, lookAtRot.y, 0, lookAtRot.w),
                                              Time.deltaTime * multiplier);
    }

    private void MoveCharacter()
    {
        if(StaticFunctions.DistanceToTarget(transform.position, _moveToPoint) > 1f && !_stopMovement)
        {
            LookToClick(_moveToPoint, 10);

            _characterController.SimpleMove(transform.forward * _speed);
            _animator.SetBool("Movement", true);
        }
        else
        {
            _animator.SetBool("Movement", false);
        }
    }

    private void DeleteMovePointLocation()
    {
        if (StaticFunctions.DistanceToTarget(transform.position, _moveToPoint) < 1f || _stopMovement && _movePointLocation != null)
        {
            Destroy(_movePointLocation.gameObject);
            _movePointLocation = null;
        }
    }
}
