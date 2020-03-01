using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MultiArrow: MonoBehaviour
{
    public Image healthImage;           // Green over red in UI is an image.
    public Slider slider;              // The slider on UI_Health Slider
    PlayerWeapons PlayerWeapons;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void MultiArrowUsed()
    {

    }
}
