using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DisplayScript))]
public class ObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DisplayScript myTarget = (DisplayScript)target;

        if (GUILayout.Button("Generate"))
            myTarget.Generate();
    }
}

