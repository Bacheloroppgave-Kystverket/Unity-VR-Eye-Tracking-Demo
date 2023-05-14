using Facebook.WitAi;
using Oculus.Installer.ThirdParty.TinyJson;
using Oculus.Platform;
using OpenCover.Framework.Model;
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

    [Header("Configuration fields")]
    [SerializeField, Tooltip("The web path")]
    private string path;

    [SerializeField, Tooltip("The port")]
    private int port;

    [SerializeField, Tooltip("The /sessions or other values.")]
    private string endPath;

    [Header("Debug fields")]
    [SerializeField, Tooltip("The path variable for the request")]
    private string pathVariable;

    [SerializeField, Tooltip("The trackable object")]
    private T value;

    [SerializeField, Tooltip("The type of the server request.")]
    private WebOptions webOption;

    [SerializeField, Tooltip("True if the process was a success")]
    private bool successful;

    /// <summary>
    /// Sets the path and port of this request.
    /// </summary>
    /// <param name="path">the path</param>
    /// <param name="port">the port</param>
    public void SetPathAndPort(string path, int port)
    {
        this.path = path;
        this.port = port;
    }

    /// <summary>
    /// Gets if the server reuqest is done successful.
    /// </summary>
    /// <returns></returns>
    public bool GetSuccessful() => successful;

    /// <summary>
    /// Sets the path variable
    /// </summary>
    /// <param name="pathVariable"></param>
    public void SetPathVariable(string pathVariable) {
        this.pathVariable = pathVariable;
    }

    /// <summary>
    /// Sets it to a post request.
    /// </summary>
    public void SetPost() {
        webOption = WebOptions.POST;
    }

    /// <summary>
    /// Sets it to a get request.
    /// </summary>
    public void SetGet() {
        webOption = WebOptions.GET;
    }

    /// <summary>
    /// Uploads the data to the address.
    /// </summary>
    /// <param name="token">the token</param>
    /// <param name="isAuthentication">the authentication</param>
    /// <returns>Enumerator that waits for the result</returns>
    public IEnumerator SendCurrentData(String token) {

        string finalPath = GetPath();
        bool validRequest = false;
        UnityWebRequest request;
        DownloadHandler downloadHandler = new DownloadHandlerBuffer();

        if (webOption == WebOptions.POST) {
            string toEncode = value == null ? "" : JsonUtility.ToJson(value);
            UploadHandler uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(toEncode));
            request = new UnityWebRequest(finalPath, "POST", downloadHandler, uploadHandler);
        } else {
            request = new UnityWebRequest(finalPath, "GET", downloadHandler, null);
        }
        if (token != "")
        {
            validRequest = true;
            request.SetRequestHeader("Authorization", "Bearer " + token);

        }
        else
        {
            throw new IllegalArgumentException("The token cannot be empty " + token);
        }

        if (validRequest) {
            yield return request.SendWebRequest();

            if (WebOptions.GET == webOption)
            {
                UnderstandGetRequest(request);
            }
            else
            {
                UnderstandPostRequest(request);
            }
            request.Dispose();
            yield return new WaitForSeconds(1);
        }
    }

    /// <summary>
    /// Constructs the path of the request.
    /// </summary>
    /// <returns>the path</returns>
    private string GetPath() {
        string pathVariable = "";
        if (webOption == WebOptions.GET && this.pathVariable != null && this.pathVariable.Trim().Length > 0)
        {
            pathVariable = "/" + this.pathVariable;
        }
        return path + ":" + port + "/" + endPath + pathVariable;
    }

    /// <summary>
    /// Gets the value of the server request
    /// </summary>
    /// <returns>the value</returns>
    protected T GetValue() { 
        return value;
    }

    /// <summary>
    /// Sends the get request.
    /// </summary>
    /// <param name="token">the token</param>
    /// <returns>the enumerator</returns>
    public IEnumerator SendGetRequest(string token) {
        SetGet();
        return SendCurrentData(token);
    }

    /// <summary>
    /// Gets the data from the wanted soruce and sets it as this objects data.
    /// </summary>
    /// <param name="request">the request</param>
    private void UnderstandGetRequest(UnityWebRequest request) {
        bool success = false;
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            HandleData(request);
            success = true;
        }
        this.successful = success;
    }

    /// <summary>
    /// Handles the incoming data.
    /// </summary>
    /// <param name="unityWebRequest">the unity web request</param>
    protected virtual void HandleData(UnityWebRequest unityWebRequest) {

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
        CheckIfObjectIsNull(value, "value");
        this.value = value;
    }

    /// <summary>
    /// Checks if the object is null or not. Throws an exception if the object is null.
    /// </summary>
    /// <param name="objecToCheck">the object to check</param>
    /// <param name="error">the error to be in the string.</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the object to check is null.</exception>
    private void CheckIfObjectIsNull(object objecToCheck, string error)
    {
        if (objecToCheck == null)
        {
            throw new IllegalArgumentException("The " + error + " cannot be null.");
        }
    }
}
