using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// This class can be placed on any GameObject and provides an API for storing weapons.
/// </summary>
public class WeaponUser : BetterMonoBehaviour
{

    public Weapon _weapon;
    public bool _canLootWeapons;
    public GameObject _storage;

    public bool LootWeapon(Loot container)
    {
        Assert.IsNotNull(_storage, "Weapon storage must be defined for a weapon to be equipped!");
        Assert.IsNotNull(container);
        if (!_canLootWeapons)
        {
            return false;
        }
        if (!(container._loot is Weapon))
        {
            return false;
        }
        if (_weapon == null)
        {
            _weapon = (Weapon) container._loot;
            _weapon.MoveToParent(_storage);
            Destroy(container.gameObject);
            return true;
        }
        if (!container._swappable)
        {
            return false;
        }
        Weapon previousWeapon = _weapon;
        _weapon = (Weapon) container._loot;
        _weapon.MoveToParent(_storage);
        container._loot = previousWeapon;
        container._loot.MoveToParent(container._holder);
        container.ResetDespawnTimer();
        return true;
    }

}
