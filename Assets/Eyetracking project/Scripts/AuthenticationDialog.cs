using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a dialog that is shown if the authentication didnt finish.
/// </summary>
[Serializable]
public class AuthenticationDialog : Dialog
{
    [SerializeField, Tooltip("The error code")]
    private int errorCode;

    /// <summary>
    /// Gets the error code.
    /// </summary>
    /// <returns>the error code</returns>
    public int GetErrorCode() => errorCode;
}
