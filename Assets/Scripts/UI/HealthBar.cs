using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void setMaxHealth(float health)
    {
        slider.maxValue = health;
    }
    public void setHealth(float health)
    {
        slider.value = health;
    }

    private void Update()
    {
        if (slider.value <= 0) 
        {
            GameManager.instance.backToMenu();
        }
    }
}
