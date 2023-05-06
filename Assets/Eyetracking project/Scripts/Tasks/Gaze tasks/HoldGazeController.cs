using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
