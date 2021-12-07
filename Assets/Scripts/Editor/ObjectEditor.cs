using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(Transform))]
public class ObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Transform myTarget = (Transform)target;
        var value = myTarget.rotation;
        int _x = (sbyte)(value.x * 127f);
        int _y = (sbyte)(value.y * 127f);
        int _z = (sbyte)(value.z * 127f);
        int _w = (sbyte)(value.w * 127f);
        //myTarget.rotation = new Quaternion((float)_x / 127f, (float)_y / 127f, (float)_z / 127f, (float)_w / 127f).normalized;
        myTarget.eulerAngles = EditorGUILayout.Vector3Field("EulerAngles", myTarget.eulerAngles);
        EditorGUILayout.LabelField("LowPrecisionQuaternion: " + new Quaternion((float)_x / 127f, (float)_y / 127f, (float)_z / 127f, (float)_w / 127f).normalized);
        EditorGUILayout.LabelField("LowPrecisionQuaternion: " + new Quaternion((float)_x / 127f, (float)_y / 127f, (float)_z / 127f, (float)_w / 127f).normalized.eulerAngles);
    }
}

