using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteract : MonoBehaviour, IInteractable
{
    public bool Interact(PlayerController player)
    {
        Debug.Log("Hi I'm " + transform.name);
        return true;
    }
}
