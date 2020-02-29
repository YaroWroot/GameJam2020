using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Loot : BetterMonoBehaviour, IInteractable
{

    public GameObject _holder;
    public Pickupable _loot;
    public bool _swappable;
    public float _despawnDelay;
    public float _despawnTimer;
    public bool _despawning;

    public void Initialise(float delay, bool swappable)
    {
        Assert.IsTrue(delay > 0);
        _despawnDelay = delay;
        _swappable = swappable;
        ResetDespawnTimer();
    }

    void FixedUpdate()
    {
        if (_despawning)
        {
            Destroy(gameObject);
            return;
        }
        if (_despawnTimer > 0)
        {
            _despawnTimer -= Time.fixedUnscaledDeltaTime;
            return;
        }
        _despawning = true;
    }

    /// <summary>
    /// Defer the decision of how the loot is handled to the loot itself.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool Interact(PlayerController player)
    {
        Assert.IsNotNull(player);
        if (_despawning)
        {
            return false;
        }
        return _loot.Interact(player, this);
    }

    public void Despawn()
    {
        _despawning = true;
    }

    public void ResetDespawnTimer()
    {
        _despawnTimer = _despawnDelay;
        _despawning = false;
    }

}
