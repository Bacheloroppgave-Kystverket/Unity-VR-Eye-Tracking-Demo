using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a hold gaze task and controls this task.
/// </summary>
public class HoldGazeController : TimedTaskController, Trackable
{

    ///<inheritdoc/>
    public void OnGazeEnter()
    {
        if (TaskManager.GetTaskManager().GetCurrentTask() == GetTask().GetTaskOrder() && GetTask().IsForceTaskOrder())
        {
            StartCoroutine(StartTimer());
        }
        else if (!GetTask().IsForceTaskOrder())
        {
            StartCoroutine(StartTimer());
        }
    }

    ///<inheritdoc/>
    public void OnGazeExit()
    {
        StopTimer();
    }
}
