using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPotions : MonoBehaviour
{
    public Text healthPotions;
    public int healthPotionsAmo;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthPotions.text = healthPotionsAmo.ToString();
    }
}
