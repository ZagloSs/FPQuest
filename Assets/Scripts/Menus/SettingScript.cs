using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{
    [SerializeField] private Slider sliderMusica;
    [SerializeField] private Slider sliderSfx;
    private void Start()
    {
        sliderMusica.value = PlayerPrefs.GetFloat("musicVol", 1);
        sliderSfx.value = PlayerPrefs.GetFloat("sfxVol", 1);
    }
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
