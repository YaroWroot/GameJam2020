using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ------------------------------------------------------------
/// <summary>
/// Pickupable serves as a template for all items that can be
/// picked up from the ground. This should always be placed on
/// the most parent object possible, because the object that
/// holds this script will be tranferred to the picker-upper.
/// </summary>
public abstract class Pickupable : BetterMonoBehaviour
{

    /// <summary>
    /// Allows the loot item to have some autonomy on what happens when
    /// a player attempts to interact with this item.
    /// </summary>
    /// <param name="player">The player who's attempting to interact with this item.</param>
    /// <param name="container">The loot container that's holding this item.</param>
    /// <returns>Returns whether the item was successfully interacted with.</returns>
    public abstract bool Interact(PlayerController player, Loot container);

}
