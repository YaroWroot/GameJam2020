using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _speed = 10.0f;
    public GameObject _playerAsset;
    public CharacterController _characterController;

    Vector3 _moveToPoint;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            LocateHit();
        }
 
        MoveCharacter();
    }

    private void LocateHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            _moveToPoint = hit.point;
        }
    }

    private void MoveCharacter()
    {
        if(Vector3.Distance((transform.position - new Vector3(0, transform.position.y, 0)), _moveToPoint) > 1)
        {
            Quaternion lookAtRot = Quaternion.LookRotation(_moveToPoint - transform.position, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  new Quaternion(0, lookAtRot.y, 0, lookAtRot.w),
                                                  Time.deltaTime * 10);

            _characterController.SimpleMove(transform.forward * _speed);
        }
    }
}
