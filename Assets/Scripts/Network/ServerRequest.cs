using Oculus.Installer.ThirdParty.TinyJson;
using Oculus.Platform;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Sends basic data to a backend.
/// </summary>
/// <typeparam name="T">is the type of the data. Example is sessionController or trackable object</typeparam>
[Serializable]
public class ServerRequest<T>{

    private UnityWebRequest webRequest;

    [SerializeField, Tooltip("The web path")]
    private string path;

    [SerializeField, Tooltip("The port")]
    private int port;

    [SerializeField, Tooltip("The /sessions or other values.")]
    private string endPath;

    [SerializeField, Tooltip("The path variable for the request")]
    private string pathVariable;

    [SerializeField, Tooltip("The trackable object")]
    private T value;

    [SerializeField, Tooltip("The type of the server request.")]
    private WebOptions webOption;

    [SerializeField]
    private bool setValue = false;


    /// <summary>
    /// Uploads the data to the address.
    /// </summary>
    /// <returns>Enumerator that waits for the result</returns>
    public IEnumerator SendCurrentData() {
        yield return new WaitForSeconds(1);
        string pathVariable = "";
        if (webOption == WebOptions.GET && this.pathVariable != null && this.pathVariable.Trim().Length > 0) {
            pathVariable = "/" + this.pathVariable;
        }
        string finalPath = path + ":" + port + "/" + endPath + pathVariable;
        Debug.Log(finalPath);
        UnityWebRequest request;
        DownloadHandler downloadHandler = new DownloadHandlerBuffer();
        if (webOption == WebOptions.POST) {
            UploadHandler uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(JsonUtility.ToJson(value)));
            request = new UnityWebRequest(finalPath, "POST", downloadHandler, uploadHandler);
        } else {
            request = new UnityWebRequest(finalPath, "GET", downloadHandler, null);
        }
        
        yield return request.SendWebRequest();

        if (WebOptions.GET == webOption) {
            UnderstandGetRequest(request);
        } else {
            UnderstandPostRequest(request);
        }
        request.Dispose();
    }

    /// <summary>
    /// Gets the data from the wanted soruce and sets it as this objects data.
    /// </summary>
    /// <param name="request">the request</param>
    private void UnderstandGetRequest(UnityWebRequest request) {
        if (request.result != UnityWebRequest.Result.Success) {
            Debug.Log(request.error);
        } else {
            T data = JsonUtility.FromJson<T>(request.downloadHandler.text);
            if (value is SimulationSetup setup && data is SimulationSetup simulationSetup) {
                setup.UpdateSimulationSetup(simulationSetup);
            }
        }
    }

    /// <summary>
    /// Sends a post request.
    /// </summary>
    /// <param name="request">the request</param>
    private void UnderstandPostRequest(UnityWebRequest request) {
        if (request.result != UnityWebRequest.Result.Success) {
            Debug.Log(request.error);
        } 
    }

    /// <summary>
    /// Sets the data to be sent.
    /// </summary>
    /// <param name="value">the data to be sent</param>
    public void SetData(T value) {
        if (!setValue) {
            this.value = value;
            this.setValue = true;
        }
    }
}
