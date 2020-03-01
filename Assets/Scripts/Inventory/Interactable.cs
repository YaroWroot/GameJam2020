using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : BetterMonoBehaviour
{

    [SerializeField]
    private Collider _field;
    
    void Start()
    {
        if (tag != "Interactable")
        {
            Debug.LogError("Interactable is not tagged as Interactable... fix that!", gameObject);
            tag = "Interactable";
        }
        if (_field == null)
        {
            Debug.LogError("Interactable doesn't have a collider... fix that!", gameObject);
            _field = gameObject.AddComponent<SphereCollider>();
            _field.isTrigger = true;
        }
    }

    /// <summary>
    /// This method is called when a player
    /// </summary>
    /// <param name="player">
    /// 
    /// </param>
    /// <returns></returns>
    public abstract bool Interact(PlayerController player);

}
