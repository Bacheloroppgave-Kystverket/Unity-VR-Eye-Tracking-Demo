using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a class that holds for how long you have looked at a location. 
/// </summary>
[Serializable]
public class GazeData{

    [SerializeField]
    private string locationID;

    [SerializeField]
    private int fixations;

    [SerializeField]
    private float fixationDuration;

    /// <summary>
    /// Makes an instance of the GazeData object
    /// </summary>
    /// <param name="locationID">the id of the location</param>
    public GazeData(string locationID) {
        CheckIfStringIsValid(locationID);
        this.locationID = locationID;
    }

    /// <summary>
    /// Increments the fixation amount
    /// </summary>
    public void IncrementFixation() {
        fixations++;
    }

    /// <summary>
    /// Adds time to the gaze data.
    /// </summary>
    public void AddTime() {
        fixationDuration += Time.deltaTime;
    }



    /// <summary>
    /// Gets the amount of fixations.
    /// </summary>
    /// <returns>The fixations</returns>
    public int GetFixations() => fixations;

    /// <summary>
    /// Gets the total fixation duration
    /// </summary>
    /// <returns>the fixation duration</returns>
    public float GetFixationDuration() => fixationDuration;

    /// <summary>
    /// Gets the location ID.
    /// </summary>
    /// <returns>the location ID</returns>
    public string GetLocationID() => locationID;

    /// <summary>
    /// Checks if a string is of invalid format.
    /// </summary>
    /// <param name="wordToCheck">the word to check</param>
    private void CheckIfStringIsValid(string wordToCheck) {
        if (string.IsNullOrEmpty(wordToCheck)) {
            throw new ArgumentNullException("The location cant be null or a empty string");
        }
    }

}
