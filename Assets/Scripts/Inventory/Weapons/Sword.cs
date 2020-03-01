using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{

    public override bool Use(WeaponUser player, Health against)
    {
        against.TakeDamage(3);
        return true;
    }

}
