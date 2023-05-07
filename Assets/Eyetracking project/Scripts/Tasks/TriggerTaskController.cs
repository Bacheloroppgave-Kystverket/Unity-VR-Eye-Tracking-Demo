using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTaskController : TaskController
{
    [SerializeField, Tooltip("The task")]
    private SimpleTask simpleTask;

    /// <summary>
    /// Does the task so that the conditions are done.
    /// </summary>
    private void DoTask() {
        if (simpleTask.IsComplete()) {
            if (simpleTask.IsForceTaskOrder() && TaskManager.GetTaskManager().GetCurrentTask() == simpleTask.GetTaskOrder() || !simpleTask.IsForceTaskOrder())
            {
                simpleTask.SetDone();
                CompleteTask();
            }
            
        }
    }

    ///<inheritdoc/>
    protected override Task GetTask()
    {
        return simpleTask;
    }

    
}
