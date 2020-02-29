using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteract : MonoBehaviour, IInteractable
{
    public bool Interact()
    {
        Debug.Log("Hi I'm " + transform.name);
        return true;
    }
}
