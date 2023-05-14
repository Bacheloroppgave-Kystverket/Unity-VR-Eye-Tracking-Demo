using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a trackable object.
/// </summary>
[Serializable]
public class TrackableObject
{
    [Header("Configure object")]
    [SerializeField, Tooltip("The name of the object")]
    private string nameOfObject;

    [SerializeField, Tooltip("Defines the type of object that we are looking at.")]
    private TrackableType trackableType = TrackableType.UNDEFINED;

    [Header("Other values")]
    [SerializeField, Tooltip("The unique id of this trackable object.")]
    private long trackableObjectID;

    /// <summary>
    /// Sets the name of the object.
    /// </summary>
    /// <param name="nameOfObject">the new object name</param>
    public void SetGameObjectName(string nameOfObject) {
        CheckIfStringIsValid(nameOfObject, "name of object");
        this.nameOfObject = nameOfObject;
    }

    /// <summary>
    /// Sets the trackable object id.
    /// </summary>
    /// <param name="trackableObjectID">the new trackable object id</param>
    public void SetTrackableObjectId(long trackableObjectID) {
        CheckIfNumberIsAboveZero(trackableObjectID, "trackable object id");
        this.trackableObjectID = trackableObjectID;
    }

    /// <summary>
    /// Gets the trackable object type
    /// </summary>
    /// <returns>the trackable object type</returns>
    public TrackableType GetTrackableType()
    {
        return trackableType;
    }

    /// <summary>
    /// Gets the name of the object
    /// </summary>
    /// <returns>the name of the object</returns>
    public string GetNameOfObject()
    {
        return nameOfObject;
    }

    /// <summary>
    /// Gets the trackable object id.
    /// </summary>
    /// <returns>the id of the trackable object</returns>
    public long GetTrackableObjectId() {
        return trackableObjectID;
    }

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

    /// <summary>
    /// Checks if a number is above zero.
    /// </summary>
    /// <param name="number">the number to check.</param>
    /// <param name="error">the error prefix.</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the number is negative.</exception>
    private void CheckIfNumberIsAboveZero(float number, string error) {
        if (number < 0) {
            throw new IllegalArgumentException("The " + error + " must be above zero.");
        }
    }
}
