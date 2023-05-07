using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedTaskController : TaskController
{

    [SerializeField, Tooltip("The timed task configuration")]
    private TimedTask timedTask;

    [SerializeField, Tooltip("Stops the timer")]
    private bool stopTimer;

    /// <summary>
    /// Sets the timed task.
    /// </summary>
    /// <param name="task">the timed task</param>
    public void SetTimedTask(TimedTask task)
    {
        this.timedTask = task;
    }

    ///<inheritdoc/>
    public override Task GetTask()
    {
        return timedTask;
    }


    /// <summary>
    /// Starts the timer of the gaze task controller.
    /// </summary>
    /// <returns>Enumerator</returns>
    protected IEnumerator StartTimer()
    {
        stopTimer = false;

        while (!stopTimer && timedTask.IsBelowThreshold())
        {
            timedTask.Addtime(Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        bool validTime = !timedTask.IsBelowThreshold();
        if (validTime)
        {
            CompleteTask();
        }
        if (!validTime && timedTask.IsReset())
        {
            timedTask.ResetTime();
        }
    }

    /// <summary>
    /// Stops the timer.
    /// </summary>
    protected void StopTimer() {
        this.stopTimer = true;
    }
}
