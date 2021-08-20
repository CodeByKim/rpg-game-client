using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TreeCreater))]
public class TreeCreaterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TreeCreater creater = target as TreeCreater;

        if (GUILayout.Button("Create Trees..."))
        {
            creater.Create();
        }
    }
}
