using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float _damage = 2;
    public float _speed = 5f;
    public string _owner;

    private void Awake()
    {
        Destroy(gameObject, 10.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Health>() != null && other.tag != _owner)
        {
            Debug.Log(other.gameObject);
            other.gameObject.GetComponent<Health>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
