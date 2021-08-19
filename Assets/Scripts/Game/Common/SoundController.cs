using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip deadSound;

    public static SoundController Instance;

    private AudioSource mAudio;
    
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        mAudio = GetComponent<AudioSource>();
    }
    
    public void PlaySoundFx(string name)
    {
        switch (name)
        {
            case "Attack":                
                mAudio.PlayOneShot(attackSound);
                break;

            case "Hit":
                mAudio.PlayOneShot(hitSound);
                break;

            case "Dead":
                mAudio.PlayOneShot(deadSound);
                break;
        }
    }
}
