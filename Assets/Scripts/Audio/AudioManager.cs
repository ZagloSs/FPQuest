using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("Audio Source")]
    [SerializeField] private AudioSource musicaSource;  
    [SerializeField] private AudioSource SFXSource;


    [Header("Audio Clips")]
    [SerializeField] private AudioClip background;
    [SerializeField] private AudioClip menus;
    [SerializeField] private AudioClip bossBattle;
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip pickUpItem;

    private bool isJugar = true;



    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {

        musicaSource.clip = menus;
        musicaSource.Play();
        musicaSource.volume = PlayerPrefs.GetFloat("musicVol", 1);
        SFXSource.volume = PlayerPrefs.GetFloat("sfxVol", 1);
    }

    private void Update()
    {

        if((SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 1) && !isJugar)
        {
            isJugar = true;
            musicaSource.clip = background;
            musicaSource.Play();
        }
        else if((SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1) && isJugar)
        {
            isJugar = false;
            musicaSource.clip = menus;
            musicaSource.Play();
        }
    }

    public void setVolumeMusica(float volume)
    {
        musicaSource.volume = volume;
        PlayerPrefs.SetFloat("musicVol", volume);
    }

    public void setVolumeSFX(float volume)
    {
        SFXSource.volume = volume;
        PlayerPrefs.SetFloat("sfxVol", volume);
    }

    public float getVolumeMusica()
    {
        return musicaSource.volume;
    }
    public float getVolumeSFX()
    {
        return SFXSource.volume;
    }



    //Llamar a efectos de sonido
    public void PlayClick()
    {
        SFXSource.PlayOneShot(click);
    }

    public void PlayPickUpItem()
    {
        SFXSource.PlayOneShot(pickUpItem);
    }
}
