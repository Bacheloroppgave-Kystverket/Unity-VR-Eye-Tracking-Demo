using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ReferencePosition
{

    [SerializeField, Tooltip("The id of the location")]
    private string locationName;

    [SerializeField, Tooltip("The time that this position has been used")]
    private float positionDuration;

    [SerializeField, Tooltip("The feedback configuration for this seat")]
    private List<FeedbackConfiguration> feedbackConfigurations;
    
    

    /// <summary>
    /// Gets the location name.
    /// </summary>
    /// <returns>the location name</returns>
    public string GetLocationName() => locationName;


    /// <summary>
    /// Gets the duration that was spent at this position.
    /// </summary>
    /// <returns>the position duration</returns>
    public float GetPositionDuration() => positionDuration;

    /// <summary>
    /// Adds time to the gaze data.
    /// </summary>
    public void AddTime(float timeToAdd)
    {
        positionDuration += Time.deltaTime;
    }

    /// <summary>
    /// Gets all the feedback configurations.
    /// </summary>
    /// <returns>a list with all the feedback configurations</returns>
    public List<FeedbackConfiguration> GetAllFeedbackConfigurations() => feedbackConfigurations; 

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
