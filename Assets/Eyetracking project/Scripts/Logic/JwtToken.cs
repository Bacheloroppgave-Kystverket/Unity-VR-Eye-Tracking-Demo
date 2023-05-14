using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The JWT token from the server.
/// </summary>
[Serializable]
public class JwtToken
{
    [SerializeField, Tooltip("The token")]
    private string token;

    /// <summary>
    /// Gets the token value of this token.
    /// </summary>
    /// <returns>the token</returns>
    public String GetToken() { 
        return token;
    }
}
