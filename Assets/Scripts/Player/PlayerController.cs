using System.Collections;
using System.Collections.Generic;
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
    private bool _inMoveTrigger = false;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _animator.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var target = LeftMouseHit();

            if(target.tag == "Enemy")
            {

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
                    MoveLocSpriteSpawn();
                }
            }
        }
        if(Input.GetMouseButton(1))
        {
            _interactableMovingTo = null;
            LocateHit();
        }
 
        MoveCharacter();
        CheckIfStoredInteractIsInRange();
        DeleteMovePointLocation();
    }

    private void CheckIfStoredInteractIsInRange()
    {
        if (_interactableMovingTo == null) return;

        if (_intersectCheck.bounds.Intersects(_interactableMovingTo.transform.GetComponent<Collider>().bounds))
        {
            _interactableMovingTo.GetComponent<IInteractable>().Interact(this);

            if (_interactableMovingTo != null) _interactableMovingTo = null;


        }
    }

    private float DistanceToTarget(Vector3 target)
    {
        return Vector3.Distance((transform.position - new Vector3(0, transform.position.y, 0)), target);
    }

    private GameObject LeftMouseHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        GameObject retVal = null;

        if (Physics.Raycast(ray, out hit, 500, ~_ignorePlayer))
        {
            retVal = hit.collider.gameObject;
        }

        return retVal;
    }

    private void LocateHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        var tempMovePoint = _moveToPoint;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.layer == _ignoreClickLayer.value)
            {
                _moveToPoint = tempMovePoint;
            }
            else
            {
                _moveToPoint = hit.point;
            }

            MoveLocSpriteSpawn();
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

    private void MoveCharacter()
    {
        if(DistanceToTarget(_moveToPoint) > 0.5f)
        {
            Quaternion lookAtRot = Quaternion.LookRotation(_moveToPoint - transform.position, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  new Quaternion(0, lookAtRot.y, 0, lookAtRot.w),
                                                  Time.deltaTime * 10);

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
        if (DistanceToTarget(_moveToPoint) < 0.5f && _movePointLocation != null)
        {
            Destroy(_movePointLocation.gameObject);
            _movePointLocation = null;
        }
    }
}
