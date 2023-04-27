using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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