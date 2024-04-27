using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void setHealth(float health)
    {
        slider.value = health;
    }

    public void setMaxValue(float maxValue)
    {
        slider.maxValue = maxValue;
    }

    private void Update()
    {
        if (slider.value <= 0) 
        {
            GameManager.instance.backToMenu();
        }
    }
}
