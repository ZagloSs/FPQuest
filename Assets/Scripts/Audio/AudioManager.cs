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

    private bool isJugar = true;



    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }

    private void Start()
    {
        musicaSource.clip = menus;
        musicaSource.Play();
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



    //Llamar a efectos de sonido
    public void PlayClick()
    {
        SFXSource.PlayOneShot(click);
    }
}
