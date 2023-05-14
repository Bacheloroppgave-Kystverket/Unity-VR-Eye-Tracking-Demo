using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple task is a task that only is completed when its triggered.
/// </summary>
[Serializable]
public class SimpleTask : Task
{
    [SerializeField, Tooltip("Is set to true when the simple task is done.")]
    private bool IsDone;

    ///<inheritdoc/>
    public override bool IsComplete()
    {
        return IsDone;
    }

    ///<inheritdoc/>
    public override void ResetTask()
    {
        IsDone = false;
    }

    /// <summary>
    /// Sets the task to be done.
    /// </summary>
    public void SetDone() { 
        IsDone = true;
    }
}
