using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Hit!");
    }
}
