using Oculus.Installer.ThirdParty.TinyJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Represents a request to get a user from the server.
/// </summary>
[Serializable]
public class UserServerRequest : ServerRequest<User>
{
    /// <summary>
    /// Handles the incoming data.
    /// </summary>
    /// <param name="unityWebRequest">the unity web request</param>
    protected override void HandleData(UnityWebRequest unityWebRequest)
    {
        SetData(JsonUtility.FromJson<User>(unityWebRequest.downloadHandler.text));
    }

    /// <summary>
    /// Gets the user.
    /// </summary>
    /// <returns>the user</returns>
    public User GetUser() {
        return GetValue();
    }

}
