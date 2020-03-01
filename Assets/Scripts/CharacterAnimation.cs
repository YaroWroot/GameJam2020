using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void Attack(int attackType)
    {
        switch (attackType)
        {
            case 0:
            default:
                _animator.SetInteger("AIAttack", 1);
                break;
            case 1:
                _animator.SetInteger("AIAttack", 2);
                break;
            case 2:
                _animator.SetInteger("AIAttack", 3);
                break;
        }
    }

    private int _playerAttackCount = 0;

    public void PlayerAttack()
    {
        _animator.SetInteger("AIAttack", _playerAttackCount + 1);

        _playerAttackCount++;

        if(_playerAttackCount == 2)
        {
            _playerAttackCount = 0;
        }
    }

    public float GetAnimationLength(string name)
    {
        var clips = _animator.runtimeAnimatorController.animationClips;
        return clips.First(e => e.name == name).length;
    }

    public void Hit()
    {
        int val = Random.Range(1, 3);
        _animator.SetInteger("Impact", val);
    }

    public void Block()
    {
        _animator.SetTrigger("Block");
    }

    public void SetMovement(bool value)
    {
        _animator.SetBool("Movement", value);
    }

    public void Death()
    {
        _animator.SetTrigger("Death");
    }

}
