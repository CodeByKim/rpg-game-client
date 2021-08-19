using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesLoader
{
    private Dictionary<string, GameObject> mPrefabs;
    private Dictionary<string, AudioClip> mSounds;

    public enum ResourceType
    {
        Prefab,
        Sound
    }

    public ResourcesLoader()
    {
        mPrefabs = new Dictionary<string, GameObject>();
        mSounds = new Dictionary<string, AudioClip>();
    }

    public void Load(ResourceType type, string path = null)
    {
        switch (type)
        {
            case ResourceType.Prefab:
                LoadPrefabs(path);
                break;

            case ResourceType.Sound:
                LoadSounds(path);
                break;
        }
    }

    public GameObject GetPrefab(string name)
    {
        return mPrefabs[name];
    }

    public AudioClip GetSound(string name)
    {
        return mSounds[name];
    }

    private void LoadPrefabs(string path = null)
    {
        string resourcePath; 
        if(path == null)
        {
            resourcePath = "Prefabs/";
        }
        else
        {
            resourcePath = System.IO.Path.Combine("Prefabs/", path);
        }
        
        GameObject[] prefabs = Resources.LoadAll<GameObject>(resourcePath);

        foreach(var item in prefabs)
        {
            string name = item.name;
            mPrefabs.Add(name, item);
        }        
    }

    private void LoadSounds(string path = null)
    {
        string resourcePath;
        if (path == null)
        {
            resourcePath = "Sounds/";
        }
        else
        {
            resourcePath = System.IO.Path.Combine("Sounds/", path);
        }

        AudioClip[] prefabs = Resources.LoadAll<AudioClip>(resourcePath);

        foreach (var item in prefabs)
        {
            string name = item.name;
            mSounds.Add(name, item);
        }
    }
}
