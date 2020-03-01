using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// ------------------------------------------------------------
/// <summary>
/// This component couples with <see cref="WeaponUser"/> to
/// define a Weapon and how it can be used.
///
/// Assuming the following Weapon object structure:
///
///     Weapon
///     ║
///     ╚══ Model
///
/// Place this class, Weapon, onto the Weapon as a component.
/// </summary>
public class Weapon : Pickupable
{

    public float _range;

    [SerializeField]
    protected PseudoTransform _equippedTransform = new PseudoTransform();

    [SerializeField]
    protected PseudoTransform _sheathedTransform = new PseudoTransform();

    [SerializeField]
    protected PseudoTransform _discardedTransform = new PseudoTransform();

    void Start()
    {
        if (_range <= 0)
        {
            Debug.LogError("Weapon range is broken... fix this!", gameObject);
        }
    }

    void Update()
    {
        Vector3 range = transform.TransformDirection(Vector3.forward) * _range;
        Debug.DrawRay(transform.position, range, Color.green);
    }

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

    public virtual bool Use(WeaponUser player, Health against)
    {
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mainHand"></param>
    /// <param name="offHand"></param>
    /// <returns>Returns whether the weapon was successfully equipped.</returns>
    public virtual bool Equip(Transform mainHand, Transform offHand)
    {
        MoveToParent(mainHand.gameObject, false);
        ApplyTransformLocally(_equippedTransform);
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sheath"></param>
    /// <returns>Returns whether the weapon was successfully sheathed.</returns>
    public virtual bool Sheath(Transform sheath)
    {
        MoveToParent(sheath.gameObject, false);
        ApplyTransformLocally(_sheathedTransform);
        return true;
    }

    public virtual bool Discard(Transform location)
    {
        MoveToParent(location.gameObject, false);
        ApplyTransformLocally(_discardedTransform);
        return true;
    }

}
