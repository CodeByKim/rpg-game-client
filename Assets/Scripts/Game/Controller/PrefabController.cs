using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabController : MonoBehaviour, IResourceController
{
    private ResourcesLoader mLoader;

    public void OnInitialize(ResourcesLoader loader)
    {
        mLoader = loader;
        mLoader.Load(ResourcesLoader.ResourceType.Prefab, "Prefabs");        
    }
}
