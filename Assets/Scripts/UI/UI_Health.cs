using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Health : MonoBehaviour
{

    public Health playerHealth;         // Health is the current & Max Health.
    public Image healthImage;           // Green over red in UI is an image.
    private Slider slider;              // The slider on UI_Health Slider


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
        float fillValue = playerHealth._hp / playerHealth._maxHp;
        slider.value = fillValue;
    }
}
