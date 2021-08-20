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

    public GameObject GetPrefab(string name)
    {
        return mLoader.GetPrefab(name);
    }

    public GameObject Create(string name)
    {
        GameObject prefab = mLoader.GetPrefab(name);
        return InstantiatePrefab(prefab, prefab.transform.position);
        
    }

    public GameObject Create(string name, Vector3 position)
    {
        GameObject prefab = mLoader.GetPrefab(name);
        return InstantiatePrefab(prefab, position);
    }

    private GameObject InstantiatePrefab(GameObject prefab, Vector3 position)
    {
        return Instantiate(prefab, position, prefab.transform.rotation);
    }
}
