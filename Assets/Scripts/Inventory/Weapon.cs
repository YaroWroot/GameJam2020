using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Weapon : Pickupable
{

    /// <summary>
    /// Checks whether the loot is a weapon, and if so, defers the decision
    /// of whether to equip it to the player's WeaponUser component.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="container"></param>
    /// <returns>Returns whether the weapon was successfully equipped.</returns>
    public sealed override bool Interact(PlayerController player, Loot container)
    {
        Assert.IsNotNull(player);
        Assert.IsNotNull(container);
        WeaponUser user = player.GetComponent<WeaponUser>();
        if (user == null)
        {
            return false;
        }
        return user.LootWeapon(container);
    }

}
