using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(PlayerEyetrackingConfig),true)]
public class PlayerConfigurationInspector : Editor
{

    public override void OnInspectorGUI()
    {
        PlayerEyetrackingConfig config = (PlayerEyetrackingConfig)target;
        DrawDefaultInspector();
        if (GUILayout.Button("Update values")) {
            Debug.Log("Values updated");
            config.UpdatePlayer();
        }
    }
}