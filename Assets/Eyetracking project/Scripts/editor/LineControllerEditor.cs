using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LineController), true)]
public class LineControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LineController lineController = (LineController)target;
        DrawDefaultInspector();
        if (GUILayout.Button("Show line"))
        {
            //lineController.DrawLine();
        }
    }
}
