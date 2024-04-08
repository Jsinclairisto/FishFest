using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;


    public AudioClip mainMusic;
    public AudioClip menuMusic;
    public AudioClip endMusic;
    public PlayerMovement playerDeath;
    public AudioClip bulletHit;
    public AudioClip death;
    public AudioClip swim;
    public AudioClip bulletShoot;
    public AudioClip squidShoot;

    void Start()
    {
        //musicSource.clip = mainMusic;
        musicSource.clip = mainMusic;
        musicSource.Play();
    }

    void Update()
    {
        if (playerDeath.isDead == true) 
        {
            musicSource.Stop();
        }
    }

    public void PlaySFX(AudioClip clip) 
    {
        SFXSource.PlayOneShot(clip);
    }
}
