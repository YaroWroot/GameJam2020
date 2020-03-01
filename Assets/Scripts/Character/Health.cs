using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float _hp = 50.0f;
    public float _maxHp = 100.0f;
    public float _ap = 1.0f;

    private CharacterAnimation _characterAnimation;

    private void Awake()
    {
        _characterAnimation = GetComponent<CharacterAnimation>();
    }

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
        var rDamage = ArmourReduction(damage);
        _hp -= rDamage;

        PointsPopUp.Create(transform.position, (float)System.Math.Round(rDamage, 2));

        if (_hp <= 0)
        {
            _characterAnimation.Death();

            gameObject.tag = "Untagged";

            StartCoroutine(DeathDestroy(_characterAnimation.GetAnimationLength("A_Gladiator_Death")));
            return true;
        }

        _characterAnimation.Hit();

        return true;
    }

    private IEnumerator DeathDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
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
