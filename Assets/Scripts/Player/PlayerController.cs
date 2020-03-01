using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum State
    {
        Normal,
        Attacking
    }

    public float _speed = 10.0f;
    public LayerMask _ignoreClickLayer;
    public LayerMask _ignorePlayer;
    public GameObject _playerAsset;
    public GameObject _pauseMenu;
    public CharacterController _characterController;
    public CharacterAnimation _characterAnimation;
    public Collider _intersectCheck;
    public Animator _animator;

    private bool _stopMovement = false;
    private GameObject _enemyInTrigger;
    private PlayerWeapons _playerWeapons;
    private State _state = State.Normal;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _animator.enabled = true;

        _playerWeapons = GetComponentInChildren<PlayerWeapons>();
    }

    private void OnDestroy()
    {
        _pauseMenu.transform.GetComponentsInChildren<Transform>().First(e => e.name == "Resume").gameObject.SetActive(false);
        
        _pauseMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        LookAtMouse();
        if (Input.GetMouseButton(1))
        {
            _stopMovement = false;
        }
        else
        {
            _stopMovement = true;
        }


        if (Input.GetMouseButtonDown(0))
        {
            _stopMovement = false;
            var target = LeftMouseHit();

            if (target.tag == "Interactable")
            {
                if (_intersectCheck.bounds.Intersects(target.transform.GetComponent<Collider>().bounds))
                {
                    target.GetComponent<IInteractable>().Interact(this);

                }
            }
            else
            {
                _playerWeapons.FireBow(_animator);
                //_animator.SetTrigger("Attack");
                //if (_enemyInTrigger != null) _playerWeapons.AttackNormal(_enemyInTrigger);
            }
        }

        if (Input.GetKey(KeyCode.Q))
        {
            _playerWeapons.FireMultipleBow(_animator);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
        }

        //if (Input.GetMouseButton(1))
        //{
        //    _stopMovement = false;
        //    _interactableMovingTo = null;
        //    LocateHit();
        //}
        MoveCharacter();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy") _enemyInTrigger = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        _enemyInTrigger = null;
    }

    private void LookAtMouse()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if(Physics.Raycast(camRay, out raycastHit))
        {
            Vector3 playerToMouse = raycastHit.point - transform.position;
            playerToMouse.y = 0;

            Quaternion look = Quaternion.LookRotation(playerToMouse);
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                              new Quaternion(0, look.y, 0, look.w),
                                              Time.deltaTime * 10);
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

    private void LookToClick(Vector3 target, float multiplier)
    {
        Quaternion lookAtRot = Quaternion.LookRotation(target - transform.position, Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation,
                                              new Quaternion(0, lookAtRot.y, 0, lookAtRot.w),
                                              Time.deltaTime * multiplier);
    }

    private void MoveCharacter()
    {
        if(!_stopMovement)
        {
            _characterController.SimpleMove(transform.forward * _speed);
            _animator.SetBool("Movement", true);
        }
        else
        {
            _animator.SetBool("Movement", false);
        }
    }
}
