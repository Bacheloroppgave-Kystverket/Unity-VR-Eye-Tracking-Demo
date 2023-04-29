using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Represents a position that a user can be in space. 
/// </summary>
[RequireComponent(typeof(ReferencePositionIdentifier))]
public class ReferencePositionController : MonoBehaviour
{

    [SerializeField, Tooltip("The reference position")]
    private ReferencePosition referencePosition;

    [SerializeField, Tooltip("The record of this position")]
    private PositionRecord positionRecord;

    private void Awake() {
        gameObject.tag = typeof(ReferencePositionController).Name;
        this.positionRecord = new PositionRecord(referencePosition);
        if (this.referencePosition.GetLocationName() == "") {
            this.referencePosition.SetLocationName(gameObject.name);
        } ;
        CheckIfListIsValid("feedbackconfigurations", referencePosition.GetCategoryConfigurationsForPosition().Any());
    }

    /// <summary>
    /// Gets the position record.
    /// </summary>
    /// <returns>the position record</returns>
    public PositionRecord GetPositionRecord()
    {
        return positionRecord;
    }

    /// <summary>
    /// Gets the location ID.
    /// </summary>
    /// <returns>the location ID</returns>
    public string GetLocationId() => referencePosition.GetLocationName();

    /// <summary>
    /// Gets the location name.
    /// </summary>
    /// <returns>the location name</returns>
    public string GetLocationName() => referencePosition.GetLocationName();

    /// <summary>
    /// Gets the duration that was spent at this position.
    /// </summary>
    /// <returns>the position duration</returns>
    public float GetPositionDuration() => positionRecord.GetPositionDuration();

    /// <summary>
    /// Adds time to the gaze data.
    /// </summary>
    public void AddTime() {
        positionRecord.AddTime(Time.deltaTime);
    }

    /// <summary>
    /// Gets the reference position.
    /// </summary>
    /// <returns>the reference position.</returns>
    public ReferencePosition GetReferencePosition() => referencePosition;

    /// <summary>
    /// Checks if the number is higher than x.
    /// </summary>
    /// <param name="number">the number to check.</param>
    /// <param name="error">the error string</param>
    /// <param name="higherThan">the number that the input number should be equal to or below.</param>
    /// <exception cref="IllegalArgumentException"></exception>
    private void CheckIfNumberIsHigherThanX(int number, string error, int higherThan)
    {
        CheckIfNumberIsValid(number, error);
        if (number < higherThan)
        {
            throw new IllegalArgumentException(error + " cannot be higher than the feedback configurations.");
        }
    }

    /// <summary>
    /// Checks if the list has any elements.
    /// </summary>
    /// <param name="error">the error to display</param>
    /// <param name="hasAny">true if the list has any. False otherwise</param>
    private void CheckIfListIsValid(string error, bool hasAny)
    {
        if (!hasAny)
        {
            Debug.Log("<color=red>Error:</color>" + error + " cannot be emtpy", gameObject);
        }
    }

    /// <summary>
    /// Checks if the number is under zero. Throws exception if the number is less than zero.
    /// </summary>
    /// <param name="number">the number to check.</param>
    /// <param name="error">the error string</param>
    /// <exception cref="IllegalArgumentException">gets thrown when the number is less than zero</exception>
    private void CheckIfNumberIsValid(int number, string error)
    {
        if (number < 0)
        {
            throw new IllegalArgumentException(error + " cannot be below zero");
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
