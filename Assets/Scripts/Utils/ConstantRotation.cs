using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{

#pragma warning disable 0649
    [SerializeField]
    private Vector3 _rotation;
#pragma warning restore 0649

    void Update()
    {
        transform.Rotate(_rotation);
    }

}
