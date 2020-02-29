using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour, IInteractable
{

    [SerializeField]
    private GameObject _holder;
    public Pickupable _loot;
    public bool _swappable;
    public float _despawnDelay;
    public float _despawnTimer;
    public bool _despawning;

    public void Initialise(float delay, bool swappable)
    {
        if (delay <= 0)
        {
            throw new IllegalArgumentException("Cannot initialise loot: the delay was zero or less.");
        }
        _despawnDelay = delay;
        _despawnTimer = delay;
        _swappable = swappable;
    }

    void FixedUpdate()
    {
        if (_despawning)
        {
            return;
        }
        if (_despawnTimer > 0)
        {
            _despawnTimer -= Time.fixedUnscaledDeltaTime;
            return;
        }
        _despawning = true;
    }

    public bool Interact(PlayerController player)
    {
        if (_loot == null)
        {
            throw new FatalErrorException("Loot cannot handle player interaction: the loot has no loot.");
        }
        return _loot.Interact(player);
    }

}
