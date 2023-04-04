using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SimulationSetupManager), true)]
public class SimulationSetupEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SimulationSetupManager simulationSetupManager = (SimulationSetupManager)target;
        DrawDefaultInspector();
        if (GUILayout.Button("Update trackable objects"))
        {
            simulationSetupManager.UpdateCurrentTrackableObjectsForSession();
        }
        if (GUILayout.Button("Update reference positions")) {
            simulationSetupManager.UpdateCurrentPositionsForSession();
        }
    }
}
