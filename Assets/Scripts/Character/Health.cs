using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float _hp = 50.0f;
    public float _maxHp = 50.0f;
    public float _ap = 1.0f;

    public bool TakeDamage(float damage)
    {
        _hp -= ArmourReduction(damage);

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
