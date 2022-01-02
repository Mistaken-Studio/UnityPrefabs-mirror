using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MeshRenderer))]
public class LowPrecisionQuaternionEditorScript : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Transform transform = ((MeshRenderer)target).GetComponent<Transform>();
        if (GUILayout.Button("Round to low precision quaternion"))
        {
            Quaternion rotation = transform.localRotation;
            sbyte _x = (sbyte)(rotation.x * 127f);
            sbyte _y = (sbyte)(rotation.y * 127f);
            sbyte _z = (sbyte)(rotation.z * 127f);
            sbyte _w = (sbyte)(rotation.w * 127f);
            transform.localRotation = new Quaternion(_x / 127f, _y / 127f, _z / 127f, _w / 127f).normalized;
        }
    }
}
