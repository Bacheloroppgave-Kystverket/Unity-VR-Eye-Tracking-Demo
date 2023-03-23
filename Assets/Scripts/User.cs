using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User
{
    [SerializeField, Tooltip("The unique id of the user")]
    private long userId;

    private string userName;

    private string password;

    /// <summary>
    /// Sets the name of the user.
    /// </summary>
    /// <param name="username">the new username</param>
    public void setUsername(string username) {
        CheckIfStringIsValid(username, "username");
        this.userName = username;
    }

    /// <summary>
    /// Sets the id of the user.
    /// </summary>
    /// <param name="userId">the user id</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the </exception>
    public void setUserId(long userId) {
        CheckIfNumberIsAboveZero(userId, "user id");
        this.userId = userId;
    }

    /// <summary>
    /// Checks if the string is null or empty. Throws exceptions if one of these conditions are true.
    /// </summary>
    /// <param name="stringToCheck">the string to check</param>
    /// <param name="error">the error of the string</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the string to check is empty or null.</exception>
    private void CheckIfStringIsValid(string stringToCheck, string error) {
        CheckIfObjectIsNull(stringToCheck, error);
        if (stringToCheck.Trim().Length == 0) { 
            throw new IllegalArgumentException("The" + error + " cannot be empty.");
        }
    }

    /// <summary>
    /// Checks if the object is null or not. Throws an exception if the object is null.
    /// </summary>
    /// <param name="objecToCheck">the object to check</param>
    /// <param name="error">the error to be in the string.</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the object to check is null.</exception>
    private void CheckIfObjectIsNull(object objecToCheck, string error) {
        if (objecToCheck == null) { 
            throw new IllegalArgumentException("The " + error + " cannot be null.");
        }
    }

    /// <summary>
    /// Checks if the number is above zero.
    /// </summary>
    /// <param name="number">the number to check</param>
    /// <param name="error">what the number is to be shown in the error.</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the number is below zero.</exception>
    private void CheckIfNumberIsAboveZero(long number, string error) {
        if (number < 0) {
            throw new IllegalArgumentException("The " + error + " needs to be larger or equal to 0");
        }
    }
}
