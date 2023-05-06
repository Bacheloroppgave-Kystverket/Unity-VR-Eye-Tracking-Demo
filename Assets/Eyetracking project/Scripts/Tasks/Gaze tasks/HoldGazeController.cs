using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldGazeController : TimedTaskController, Trackable
{

    ///<inheritdoc/>
    public void OnGazeEnter()
    {
        StartCoroutine(StartTimer());
    }

    ///<inheritdoc/>
    public void OnGazeExit()
    {
        StopTimer();
    }
}
