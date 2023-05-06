using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TimedTask : Task
{
    [SerializeField, Tooltip("Set to true if the hold gaze resets when looking away")]
    private bool resets;

    [SerializeField, Tooltip("The threshold")]
    private float threshold;

    [SerializeField, Tooltip("The time of the object.")]
    private float time;

    /// <summary>
    /// Gets if the timed task should reset if the threshold is not met.
    /// </summary>
    /// <returns>true if the task should reset. False otherwise.</returns>
    public bool IsReset() => resets;

    /// <summary>
    /// Gets the time of the task.
    /// </summary>
    /// <returns>the time</returns>
    public float GetTime() => time;

    /// <summary>
    /// Adds time to the timed task.
    /// </summary>
    /// <param name="time">the new time</param>
    public void Addtime(float time) {
        CheckIfNumberIsAboveZero(time, "time");
        this.time += time;
    }

    /// <summary>
    /// Checks if the timed task is below the threshold.
    /// </summary>
    /// <returns>True if the time is below the threshold. False otherwise</returns>
    public bool IsBelowThreshold() {
        return time < threshold;
    }

    /// <summary>
    /// Resets the time of this timed task.
    /// </summary>
    public void ResetTime() {
        this.time = 0;
    }

    /// <summary>
    /// Checks if the number is above zero.
    /// </summary>
    /// <param name="number">the number to check</param>
    /// <param name="error">what the number is to be shown in the error.</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the number is below zero.</exception>
    private void CheckIfNumberIsAboveZero(float number, string error)
    {
        if (number < 0)
        {
            throw new IllegalArgumentException("The " + error + " needs to be larger or equal to 0");
        }
    }

    ///<inheritdoc/>
    public override bool IsComplete()
    {
        return time >= threshold;
    }

    ///<inheritdoc/>
    public override void ResetTask()
    {
        this.time = 0;
    }
}
