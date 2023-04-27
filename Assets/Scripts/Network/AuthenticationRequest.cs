using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.Rendering.DebugUI;

[Serializable]
public class AuthenticationRequest
{
    [Header("Configuration fields")]
    [SerializeField, Tooltip("The web path")]
    private string path;

    [SerializeField, Tooltip("The port")]
    private int port;

    [SerializeField, Tooltip("The /sessions or other values.")]
    private string endPath;

    [Header("Debug fields")]
    [SerializeField]
    private static JwtToken token;

    /// <summary>
    /// Constructs the path of the request.
    /// </summary>
    /// <returns>the path</returns>
    private string GetPath()
    {
        return path + ":" + port + "/" + endPath;
    }

    /// <summary>
    /// Sends a login request and sets the token if the input is valid.
    /// </summary>
    /// <param name="loginDetails">login details</param>
    /// <returns></returns>
    public IEnumerator SendLoginRequest(LoginDetails loginDetails) {
        string path = GetPath();

        UploadHandler uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(JsonUtility.ToJson(loginDetails)));
        DownloadHandler downloadHandler = new DownloadHandlerBuffer();
        UnityWebRequest unityWebRequest = new UnityWebRequest(path, "POST", downloadHandler, uploadHandler);
        yield return unityWebRequest.SendWebRequest();
        if (unityWebRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(unityWebRequest.error);
        }
        else {
            Debug.Log(unityWebRequest.downloadHandler.text);
            token = JsonUtility.FromJson<JwtToken>(unityWebRequest.downloadHandler.text);
        }

    }

    /// <summary>
    /// Gets the token.
    /// </summary>
    /// <returns>the token</returns>
    public static string GetToken() => token.GetToken();
}
