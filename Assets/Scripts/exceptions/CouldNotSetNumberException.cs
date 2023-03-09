using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gets thrown if a number could not be set.
/// </summary>
public class CouldNotSetNumberException : Exception
{
    private string message;

    /// <summary>
    /// Makes an instance of the could not set number exception.
    /// </summary>
    /// <param name="message">the error message.</param>
    public CouldNotSetNumberException(string message) : base() {
        this.message = message;
    }

    /// <summary>
    /// Gets the message.
    /// </summary>
    /// <returns>the error message</returns>
    public string GetMessage() => message;


}
