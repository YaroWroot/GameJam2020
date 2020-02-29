using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Pickupable
{

    public override bool Interact(PlayerController player)
    {
        Debug.Log("Hello, World!");
        return true;
    }

}
