using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestArmour : MonoBehaviour
{

    public TestpHealth playerArmour;    // TestpHealth is the current & Max Health.
    public Image armourImage;           // Green over red in UI is an image.
    private Slider slider;              // Slider has values between 0-100


    // Start is called before the first frame update
    void Awake()
    {
        // Slider gets the UI Slider it's attached to
        slider = GetComponent<Slider>();

        //healthImage = transform.Find("HealthFill").GetComponent<Image>();
        //healthImage.fillAmount = .3f;
    }

    // Update is called once per frame
    void Update()
    {
        // fillValue gets value of health, and sets slider value to it. 
        float fillValue = playerArmour.currentArmour / playerArmour.maxArmour;
        slider.value = fillValue;
    }
}
