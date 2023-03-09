using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a position that a user can be in space. 
/// </summary>
public class ReferencePositionController : MonoBehaviour
{

    [SerializeField, Tooltip("The reference position")]
    private ReferencePosition referencePosition;

    [SerializeField, Tooltip("The current feedback config.")]
    private int currentConfig = 0;

    private void Awake() {
        gameObject.tag = typeof(ReferencePositionController).Name;
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
    public float GetPositionDuration() => referencePosition.GetPositionDuration();

    /// <summary>
    /// Adds time to the gaze data.
    /// </summary>
    public void AddTime() {
        referencePosition.AddTime(Time.deltaTime);
    }

    /// <summary>
    /// Gets the reference position.
    /// </summary>
    /// <returns>the reference position.</returns>
    public ReferencePosition GetReferencePosition() => referencePosition;

    /// <summary>
    /// Gets the current config number.
    /// </summary>
    /// <returns>the index of the current config</returns>
    public int GetCurrentConfig() => currentConfig;

    /// <summary>
    /// Sets the current config.
    /// </summary>
    /// <param name="currentNumber">the current number</param>
    public void SetCurrentConfig(int currentNumber)
    {
        CheckIfNumberIsHigherThanX(currentNumber, "current position", referencePosition.GetAllFeedbackConfigurations().Count);
        this.currentConfig = currentNumber;
    }

    /// <summary>
    /// Gets the feedback configuration.
    /// </summary>
    /// <returns>the feedback configuration</returns>
    public FeedbackConfiguration GetCurrentFeedbackConfiguration() => referencePosition.GetAllFeedbackConfigurations()[currentConfig];

    /// <summary>
    /// Checks if the number is higher than x.
    /// </summary>
    /// <param name="number">the number to check.</param>
    /// <param name="error">the error string</param>
    /// <param name="higherThan">the number that the input number should be equal to or below.</param>
    /// <exception cref="CouldNotSetNumberException"></exception>
    private void CheckIfNumberIsHigherThanX(int number, string error, int higherThan)
    {
        CheckIfNumberIsValid(number, error);
        if (number < higherThan)
        {
            throw new CouldNotSetNumberException(error + " cannot be higher than the feedback configurations.");
        }
    }

    /// <summary>
    /// Checks if the number is under zero. Throws exception if the number is less than zero.
    /// </summary>
    /// <param name="number">the number to check.</param>
    /// <param name="error">the error string</param>
    /// <exception cref="CouldNotSetNumberException">gets thrown when the number is less than zero</exception>
    private void CheckIfNumberIsValid(int number, string error)
    {
        if (number < 0)
        {
            throw new CouldNotSetNumberException(error + " cannot be below zero");
        }
    }
}
