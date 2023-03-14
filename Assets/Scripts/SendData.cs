using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Sends basic data to a backend.
/// </summary>
/// <typeparam name="T">is the type of the data. Example is sessionController or trackable object</typeparam>
[Serializable]
public class SendData<T>{

    private UnityWebRequest webRequest;

    [SerializeField, Tooltip("The web path")]
    private string path;

    [SerializeField, Tooltip("The port")]
    private int port;

    [SerializeField, Tooltip("The /sessions or other values.")]
    private string endPath;

    [SerializeField, Tooltip("The trackable object")]
    private T value;

    [SerializeField]
    private bool setValue = false;


    /// <summary>
    /// Uploads the data to the address.
    /// </summary>
    /// <returns>Enumerator that waits for the result</returns>
    public IEnumerator SendCurrentData() {
        yield return new WaitForSeconds(1);
        string finalPath = path + ":" + port + endPath;
        DownloadHandler downloadHandler = new DownloadHandlerBuffer();
        UploadHandler uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(JsonUtility.ToJson(value)));
        UnityWebRequest request = new UnityWebRequest(finalPath, "POST",  downloadHandler, uploadHandler);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success) {
            Debug.Log(request.error);
        } else {
            Debug.Log(value.GetType() + "");
        }
        request.Dispose();
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
