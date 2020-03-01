using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class BetterMonoBehaviour : MonoBehaviour
{

    public void MoveToParent(GameObject parent, bool keepLocalTransform = true)
    {
        Assert.IsNotNull(parent, "Cannot move this object to a null parent.");
        Assert.IsFalse(parent == gameObject, "Cannot move this object to be a child of itself.");
        // Define defaults
        Vector3 localPosition = Vector3.zero;
        Quaternion localRotation = Quaternion.identity;
        Vector3 localScale = Vector3.one;
        // Keep local transforms if relevant
        if (keepLocalTransform)
        {
            localPosition = gameObject.transform.localPosition;
            localRotation = gameObject.transform.localRotation;
            localScale = gameObject.transform.localScale;
        }
        // Move the object to the new parent
        gameObject.transform.parent = parent.transform;
        // And set the relevant local metrics - this is necessary
        // otherwise the object will force local metrics so that
        // the object doesn't move in world space
        gameObject.transform.localPosition = localPosition;
        gameObject.transform.localRotation = localRotation;
        gameObject.transform.localScale = localScale;
    }

    public void ApplyTransformLocally(PseudoTransform transform)
    {
        Assert.IsNotNull(transform, "Cannot apply that transform locally if it's null.");
        gameObject.transform.localPosition = transform.position;
        gameObject.transform.localEulerAngles = transform.rotation;
        gameObject.transform.localScale = transform.scale;
    }

    public Vector3 getYNullifiedPosition()
    {
        return new Vector3(transform.position.x, 0, transform.position.z);
    }

}
