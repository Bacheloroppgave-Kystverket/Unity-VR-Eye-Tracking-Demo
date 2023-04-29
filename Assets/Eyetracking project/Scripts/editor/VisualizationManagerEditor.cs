using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VisualizationManager), true)]
public class VisualizationManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        VisualizationManager VisualizationManager = (VisualizationManager)target;
        DrawDefaultInspector();
        if (GUILayout.Button("Show gaze plot"))
        {
            
        }
    }
}
