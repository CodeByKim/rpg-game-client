using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCreater : MonoBehaviour
{
    [SerializeField] private GameObject[] treePrefab;
    [SerializeField] private int count;

    public void Create()
    {
        Transform root = GameObject.Find("Env").transform;
        GameObject parent = new GameObject("Trees");
        parent.transform.SetParent(root);

        for (int i = 0; i < count; i++)
        {
            Vector3 pos = new Vector3(Random.Range(0, 1000), 0, Random.Range(0, 1000));

            int treeType = Random.Range(0, 3);
            GameObject tree = Instantiate(treePrefab[treeType]);
            tree.transform.position = pos;
            tree.transform.SetParent(parent.transform);
        }
    }
}
