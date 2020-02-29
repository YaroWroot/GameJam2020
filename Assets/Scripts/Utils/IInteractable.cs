using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{

    /// <summary>
    /// For use with the player controller
    /// </summary>
    /// <param name="player"></param>
    /// <returns>true if interaction is a success</returns>
    bool Interact(PlayerController player);

}
