using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{
    [SerializeField] private Slider sliderMusica;
    [SerializeField] private Slider sliderSfx;
    public void submit()
    {
        AudioManager.instance.setVolumeMusica(sliderMusica.value);
        AudioManager.instance.setVolumeSFX(sliderSfx.value);
    }
    public void cancel()
    {
        sliderMusica.value = AudioManager.instance.getVolumeMusica();
        sliderSfx.value = AudioManager.instance.getVolumeSFX();
    }
}
