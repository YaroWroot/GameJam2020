using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTest : MonoBehaviour
{

    public void onValueChanged(float value)
    {
        Debug.Log ("New Value " + value);
    }

    float i;

    public float ValueChange()
    {
        i = 50.0f;
        return i;
    }
}
