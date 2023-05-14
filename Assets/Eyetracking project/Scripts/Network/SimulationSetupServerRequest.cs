using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Represents a simulation setup server request. Checks if the simulation setup is posted. If not posts it.
/// </summary>
[Serializable]
public class SimulationSetupServerRequest : ServerRequest<SimulationSetup>{

    /// <summary>
    /// Handles the incoming data.
    /// </summary>
    /// <param name="unityWebRequest">the unity web request</param>
    protected override void HandleData(UnityWebRequest unityWebRequest)
    {
        SimulationSetup value = JsonUtility.FromJson<SimulationSetup>(unityWebRequest.downloadHandler.text);
        if (value != null) {
            SimulationSetup sim = GetValue();
            sim.UpdateSimulationSetup(value);
        }
    }
}
