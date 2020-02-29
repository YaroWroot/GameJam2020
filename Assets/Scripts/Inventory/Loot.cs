using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{

    [SerializeField]
    private Pickupable _loot;
    [SerializeField]
    private long _despawnTime;
    private bool _despawning;

    public void Initialise(long delay)
    {
        if (delay <= 0)
        {
            throw new IllegalArgumentException("Cannot initialise loot: the delay was zero or less.");
        }
        _despawnTime = DateTime.Now.Millisecond + delay;
    }

    void FixedUpdate()
    {
        if (_despawning)
        {
            return;
        }
        if (DateTime.Now.Millisecond < _despawnTime)
        {
            return;
        }
        _despawning = true;
    }

}
