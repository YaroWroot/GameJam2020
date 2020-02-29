using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float _hp = 50.0f;
    public float _maxHp = 100.0f;
    public float _ap = 1.0f;

    public bool Heal(float value)
    {
        if((_hp + value) > _maxHp)
        {
            _hp = _maxHp;
            return true;
        }

        _hp += value;
        return true;
    }

    public bool TakeDamage(float damage)
    {
        _hp -= ArmourReduction(damage);

        if(_hp <= 0)
        {
            GetComponent<CharacterAnimation>().Death();
            _hp = 0;
        }

        return true;
    }

    private float ArmourReduction(float damage)
    {
        float retVal = 0;

        float reducedDmg = damage * _ap;

        retVal = (damage - reducedDmg);

        _ap -= ((reducedDmg) / 10);

        return retVal;
    }
}
