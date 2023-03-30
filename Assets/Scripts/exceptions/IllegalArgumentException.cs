using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gets thrown if the input argument is invalid.
/// </summary>
public class IllegalArgumentException : ArgumentException
{
    private string message;

    /// <summary>
    /// Makes an instance of the IllegalArgumentException.
    /// </summary>
    /// <param name="message">the error message.</param>
    public IllegalArgumentException(string message) : base() {
        CheckIfStringIsValid(message, "illegal argument message");
        this.message = message;
    }

    /// <summary>
    /// Gets the message.
    /// </summary>
    /// <returns>the error message</returns>
    public string GetMessage() => message;

    /// <summary>
    /// Checks if the string is null or empty. Throws exceptions if one of these conditions are true.
    /// </summary>
    /// <param name="stringToCheck">the string to check</param>
    /// <param name="error">the error of the string</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the string to check is empty or null.</exception>
    private void CheckIfStringIsValid(string stringToCheck, string error)
    {
        CheckIfObjectIsNull(stringToCheck, error);
        if (stringToCheck.Trim().Length == 0)
        {
            throw new IllegalArgumentException("The" + error + " cannot be empty.");
        }
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
