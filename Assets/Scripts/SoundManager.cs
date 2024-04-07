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

    public AudioClip bulletHit;
    public AudioClip death;
    public AudioClip swim;
    public AudioClip bulletShoot;
    public AudioClip squidShoot;

    void Start()
    {
        musicSource.clip = mainMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip) 
    {
        SFXSource.PlayOneShot(clip);
    }
}
