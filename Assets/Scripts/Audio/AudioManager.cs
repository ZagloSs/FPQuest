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


    [Header("Audio Music")]
    [SerializeField] private AudioClip background;
    [SerializeField] private AudioClip menus;
    [SerializeField] private AudioClip bossBattle;
    [SerializeField] private AudioClip credits;

    [Header("Audio SFX")]
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip pickUpItem;
    [SerializeField] private AudioClip magicSpelled;
    [SerializeField] private AudioClip meleeAttackSound;
    [SerializeField] private AudioClip monsterHitted;
    [SerializeField] private AudioClip openDoor;



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

    public void playBoss()
    {
        musicaSource.clip = bossBattle;
        musicaSource.Play();
    }

    public void stopmusic()
    {
        musicaSource.Stop();
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

   
    public void playCredits()
    {
        musicaSource.clip = credits;
        musicaSource.Play();

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
    public void PlayFireball()
    {
        SFXSource.PlayOneShot(magicSpelled);
    }
    public void PlayMeleeAtt()
    {
        SFXSource.PlayOneShot(meleeAttackSound);
    }

    public void PlayOpenDoor()
    {
        SFXSource.PlayOneShot(openDoor);
    }

    public void PlayEnemyHitted()
    {
        SFXSource.PlayOneShot(monsterHitted);
    }

    public void playMonsterDeathSound(AudioClip ac)
    {
        SFXSource.PlayOneShot(ac);
    }
}
