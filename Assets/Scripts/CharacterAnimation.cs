using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public Animator _animator;
    public CharacterController _characterController;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        //if (_characterController.velocity.magnitude > 0)
        //{
        //    _animator.SetBool("Movement", true);
        //}
        //else
        //{
        //    _animator.SetBool("Movement", false);
        //}
    }
}
