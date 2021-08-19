using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour, IFxController
{    
    private AudioSource mAudio;
    private ResourcesLoader mLoader;

    public void OnInitialize(ResourcesLoader loader)
    {
        mAudio = GetComponent<AudioSource>();

        mLoader = loader;
        mLoader.Load(ResourcesLoader.ResourceType.Sound);
    }

    public void Play(string name)
    {
        AudioClip clip = mLoader.GetSound(name);
        mAudio.PlayOneShot(clip);
    }
}
