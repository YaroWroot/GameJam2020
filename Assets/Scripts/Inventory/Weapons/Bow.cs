using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{

    public override bool Use(WeaponUser player, Health against)
    {
        against.TakeDamage(10);
        return true;
    }

}
