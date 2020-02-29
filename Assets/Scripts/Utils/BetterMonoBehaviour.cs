using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class BetterMonoBehaviour : MonoBehaviour
{

    public void MoveToParent(GameObject parent)
    {
        Assert.IsNotNull(parent, "Cannot move this object to a null parent.");
        Assert.IsFalse(parent == gameObject, "Cannot move this object to be a child of itself.");
        gameObject.transform.parent = parent.transform;
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.identity;
        gameObject.transform.localScale = Vector3.one;
    }

    public Vector3 getYNullifiedPosition()
    {
        return new Vector3(transform.position.x, 0, transform.position.z);
    }

}
