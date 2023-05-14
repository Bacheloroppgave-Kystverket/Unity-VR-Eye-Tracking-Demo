using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Represents a task configuration that can be used to make tasks.
/// </summary>
[Serializable]
public class TaskConfiguration
{
    [SerializeField, Tooltip("The name of the gameobject")]
    private string gameObjectName;

    [SerializeField, Tooltip("The object that needs the tasks.")]
    private GameObject objectThatNeedsTasks;

    [Header("Gaze tasks")]
    [SerializeField, Tooltip("The tasks that has a gaze period.")]
    private TimedTask gazePeriodTask;

    [SerializeField, Tooltip("The tasks that only needs to be looked at once.")]
    private SimpleTask lookAtTask;

    [Space(5), Header("Touch tasks - Only added if XRGrabable component on object")]
    [SerializeField, Tooltip("The objects that the user needs to hold for longer periods.")]
    private TimedTask holdObjectTask;

    [SerializeField, Tooltip("The objects that the user only needs to touch.")]
    private SimpleTask touchObjectTask;


    /// <summary>
    /// Gets the liveTasks that the user only needs to look at.
    /// </summary>
    /// <returns>the liveTasks</returns>
    public SimpleTask GetLookAtTask() => lookAtTask;

    /// <summary>
    /// Gets the liveTasks that the user needs to look at for a longer period.
    /// </summary>
    /// <returns>the liveTasks</returns>
    public TimedTask GetGazePeriodTask() => gazePeriodTask;

    /// <summary>
    /// Gets the object that is supposed to get liveTasks.
    /// </summary>
    /// <returns>the object that needs this task</returns>
    public GameObject GetGameObject() => objectThatNeedsTasks;

    /// <summary>
    /// Gets the hold object timed liveTasks.
    /// </summary>
    /// <returns>the liveTasks</returns>
    public TimedTask GetHoldObjectTask() => holdObjectTask;

    /// <summary>
    /// Gets the touch objects liveTasks.
    /// </summary>
    /// <returns>the liveTasks</returns>
    public SimpleTask GetTouchObjectTask() => touchObjectTask;
}
