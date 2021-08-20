using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCreater : MonoBehaviour
{
    [Header("Prefabs")]    
    [SerializeField] private GameObject[] treePrefab;
    [SerializeField] private GameObject[] plantsPrefab;    

    [SerializeField] private int treeCount;
    [SerializeField] private int plantsCount;

    public void CreateTree()
    {
        Transform root = GameObject.Find("Env").transform;
        GameObject parent = new GameObject("Trees");
        parent.transform.SetParent(root);

        for (int i = 0; i < treeCount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(0, 1000), 0, Random.Range(0, 1000));

            int treeType = Random.Range(0, treePrefab.Length);
            GameObject tree = Instantiate(treePrefab[treeType]);
            tree.transform.position = pos;
            tree.transform.SetParent(parent.transform);
        }
    }

    public void CreatePlants()
    {
        Transform root = GameObject.Find("Env").transform;
        GameObject parent = new GameObject("Plants");
        parent.transform.SetParent(root);

        for (int i = 0; i < plantsCount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(0, 1000), 0, Random.Range(0, 1000));

            int treeType = Random.Range(0, plantsPrefab.Length);
            GameObject plant = Instantiate(plantsPrefab[treeType]);
            plant.transform.position = pos;
            plant.transform.SetParent(parent.transform);
        }
    }
}
