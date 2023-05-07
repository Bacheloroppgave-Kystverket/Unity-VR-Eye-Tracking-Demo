using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LoginDetails
{
    [SerializeField, Tooltip("The username")]
    private string username;

    [SerializeField, Tooltip("The password")]
    private string password;

    /// <summary>
    /// Gets the username.
    /// </summary>
    /// <returns>the username</returns>
    public string GetUsername() => username;

    /// <summary>
    /// Gets the password.
    /// </summary>
    /// <returns>the password</returns>
    public string GetPassword() => password;
}
