using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{

    void Start()
    {
        _range = 15f;
        _equippedTransform.position.Set(-0.002f, -0.025f, 0.027f);
        _equippedTransform.rotation.Set(-74.61201f, 156.011f, 117.07f);
        _sheathedTransform.rotation.Set(41.92f, -90f, -11.69f);
    }

    public override bool Use(WeaponUser player, Health against)
    {
        against.TakeDamage(10);
        return true;
    }

}
