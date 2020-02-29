using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looter : MonoBehaviour
{

    [SerializeField]
    private Loot _nearest;
    private Vector3 _lastPosition;
    private HashSet<Loot> _touching = new HashSet<Loot>();

    // Start is called before the first frame update
    void Start()
    {
        _lastPosition = transform.position;
    }
    
    /// <summary>
    /// If the player moves and is touching loot, find the nearest
    /// loot object should there ever be an overlap.
    /// </summary>
    void FixedUpdate()
    {
        if (_touching.Count < 1)
        {
            return;
        }
        if (_lastPosition.Equals(transform.position))
        {
            return;
        }
        _lastPosition = transform.position;
        Loot nearestLoot = null;
        float nearestDistance = float.MaxValue;
        foreach (Loot touching in _touching)
        {
            float distance = Vector3.Distance(transform.position, touching.transform.position);
            if (distance >= nearestDistance)
            {
                continue;
            }
            nearestLoot = touching;
            nearestDistance = distance;
        }
        if (nearestLoot == null)
        {
            return;
        }
        _nearest = nearestLoot;
    }

    void OnCollisionEnter(Collision collision)
    {
        Loot loot = collision.gameObject.GetComponent<Loot>();
        if (loot == null)
        {
            return;
        }
        _touching.Add(loot);
    }

    void OnCollisionExit(Collision collision)
    {
        Loot loot = collision.gameObject.GetComponent<Loot>();
        if (loot == null)
        {
            return;
        }
        _touching.Remove(loot);
    }

}
