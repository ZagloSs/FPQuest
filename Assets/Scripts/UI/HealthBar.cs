using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject portrait;
    public Slider slider;

    private void Start()
    {
        Image img = portrait.GetComponent<Image>();
        img.sprite = GameManager.instance.getHealthPortrait();

        setMaxValue(GameManager.instance.getProperties()["Health"]);
        setHealth(GameManager.instance.getProperties()["Health"]);
    }

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
