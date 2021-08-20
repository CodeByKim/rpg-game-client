using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResourceController
{
    void OnInitialize(ResourcesLoader loader);    
}

public class VFXController : MonoBehaviour, IResourceController
{    
    private ResourcesLoader mLoader;

    public void OnInitialize(ResourcesLoader loader)
    {
        mLoader = loader;
        mLoader.Load(ResourcesLoader.ResourceType.Prefab, "Fx");        
    }

    public void Play(string name, Vector3 pos)
    {        
        GameObject prefab = mLoader.GetPrefab(name);

        VFX fx = Instantiate(prefab).GetComponent<VFX>();
        fx.transform.position = pos;

        Destroy(fx.gameObject, fx.PlayTime);
    }
}
