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
public abstract class Pickupable : MonoBehaviour
{

    public abstract bool Interact(PlayerController player);

}
