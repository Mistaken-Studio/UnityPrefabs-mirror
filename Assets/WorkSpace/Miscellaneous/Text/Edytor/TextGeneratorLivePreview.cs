using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Mistaken.UnityPrefabs.Text.TextGenerator))]
public class TextGeneratorLivePreview : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Mistaken.UnityPrefabs.Text.TextGenerator textGenerator = (Mistaken.UnityPrefabs.Text.TextGenerator)target;
        if (GUILayout.Button("Preview"))
        {
            textGenerator.SetText(textGenerator.DebugText);
        }
    }
}
