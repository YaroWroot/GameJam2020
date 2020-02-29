using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestHealth : MonoBehaviour
{

    public TestpHealth playerHealth;
    public Image healthImage;
    private Slider slider;


    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();

        //healthImage = transform.Find("HealthFill").GetComponent<Image>();
        //healthImage.fillAmount = .3f;
    }

    // Update is called once per frame
    void Update()
    {
        float fillValue = playerHealth.currentHealth / playerHealth.maxHealth;
        slider.value = fillValue;
    }
}
