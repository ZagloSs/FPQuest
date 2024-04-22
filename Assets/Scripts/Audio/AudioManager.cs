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
    [SerializeField] private AudioClip click;

    private AudioClip background;


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
        musicaSource.clip = background;
        musicaSource.Play();
    }


    //Llamar a efectos de sonido
    public void PlayClick()
    {
        SFXSource.PlayOneShot(click);
    }
}
