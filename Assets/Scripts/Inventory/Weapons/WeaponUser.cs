using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// This component provides an API for storing weapons.
///
/// Assuming the following Player object structure:
///
///     Player
///     ║
///     ╠══ Model
///     ║
///     ╚══ WeaponHolder
///         ║
///         ╚══ DefaultWeapon
///
/// Place this class, WeaponUser, on Player as a component, then
/// in the Editor, drag WeaponHolder into the Storage slot, and
/// DefaultWeapon into the Weapon slot.
///
/// The DefaultWeapon MUST have the <see cref="Weapon"//> class
/// as a component.
/// </summary>
public class WeaponUser : BetterMonoBehaviour
{

    public Weapon _primaryWeapon;

    public Weapon _secondaryWeapon;

    /// <summary>
    /// Prevents NPCs from looting weapons.
    /// </summary>
    public bool _disableWeaponLooting;

    /// <summary>
    /// Prevents NPCs from looting weapons.
    /// </summary>
    public Transform _mainHandPoint;

    public Transform _offHandPoint;

    public Transform _sheathPoint;

    void Start()
    {
        if (_primaryWeapon == null && _secondaryWeapon == null)
        {
            Debug.LogError("WeaponUser has neither a primary nor secondary weapon... fix that!", gameObject);
        }
        if (_mainHandPoint == null)
        {
            Debug.LogError("WeaponUser does not have a main hand spot specified, this weapon user will not equip weapons properly... fix that!", gameObject);
        }
        if (_offHandPoint == null)
        {
            Debug.LogError("WeaponUser does not have an off hand spot specified, this weapon user will not equip two handed weapons properly... fix that!", gameObject);
        }
        if (_sheathPoint == null)
        {
            Debug.LogError("WeaponUser does not have a sheath spot specific, this user will not sheath weapons properly... fix that!", gameObject);
        }
    }

    /// <summary>
    /// Swaps the primary weapon with the secondary weapon.
    /// </summary>
    /// <returns>
    /// Returns whether the primary and secondary weapon was successfully swapped.
    /// </returns>
    public bool SwapWeapons()
    {
        if (_primaryWeapon == null || _secondaryWeapon == null)
        {
            Debug.LogError("[WeaponUser] Could not swap weapons: user does not have two weapons.", gameObject);
            return false;
        }
        if (!_secondaryWeapon.Equip(_mainHandPoint, _offHandPoint))
        {
            Debug.LogError("[WeaponUser] Could not swap weapons: secondary weapon cannot be equipped.", gameObject);
            return false;
        }
        if (!_primaryWeapon.Sheath(_sheathPoint))
        {
            Debug.LogError("[WeaponUser] Could not swap weapons: primary weapon cannot be sheathed.", gameObject);
            return false;
        }
        (_primaryWeapon, _secondaryWeapon) = (_secondaryWeapon, _primaryWeapon);
        return true;
    }

    public bool LootWeapon(Loot container)
    {
        if (_primaryWeapon == null)
        {
            Debug.LogError("[WeaponUser] Could not loot weapon: primary weapon is null.", gameObject);
            return false;
        }
        if (container == null)
        {
            Debug.LogError("[WeaponUser] Could not loot weapon: loot object is null.", gameObject);
            return false;
        }
        if (_disableWeaponLooting)
        {
            return false;
        }
        if (!container._swappable)
        {
            return false;
        }
        if (!(container._loot is Weapon))
        {
            return false;
        }
        if (!_primaryWeapon.Discard(container._holder.transform))
        {
            return false;
        }
        (_primaryWeapon, container._loot) = ((Weapon) container._loot, _primaryWeapon);
        container.ResetDespawnTimer();
        return true;
    }

    public bool UseWeapon(Health against)
    {
        if (_primaryWeapon == null)
        {
            Debug.LogError("[WeaponUser] Could not use weapon: primary weapon is null.", gameObject);
            return false;
        }
        if (against == null)
        {
            Debug.LogError("[WeaponUser] Could not use weapon: enemy is null.");
            return false;
        }
        if (Vector3.Distance(getYNullifiedPosition(), against.getYNullifiedPosition()) > _primaryWeapon._range)
        {
            return false;
        }
        return _primaryWeapon.Use(this, against);
    }

}
