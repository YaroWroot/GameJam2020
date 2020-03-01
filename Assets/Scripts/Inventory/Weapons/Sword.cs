using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{

    void Awake()
    {
        _range = 2.5f;
        _equippedTransform.position.Set(0.021f, 0f, 0.0253f);
        _equippedTransform.rotation.Set(90f, 0f, 90f);
        _sheathedTransform.position.Set(-0.1f, 0.19f, 0f);
        _sheathedTransform.rotation.Set(155.43f, 90f, 0f);
    }

    public override bool Use(WeaponUser player, Health against)
    {
        against.TakeDamage(3);
        return true;
    }

}
