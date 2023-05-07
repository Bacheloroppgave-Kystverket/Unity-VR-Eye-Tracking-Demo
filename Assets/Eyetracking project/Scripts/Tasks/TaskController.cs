using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// A task controller contains a task to be performed by the user.
/// </summary>
public abstract class TaskController : MonoBehaviour {

    [SerializeField]
    private Toggle checkbox;
    
    [SerializeField]
    private UnityEvent taskUpdateCall = new UnityEvent();

    private void OnEnable() {
        this.taskUpdateCall.Invoke();
    }

    private void OnDisable() {
        this.taskUpdateCall.Invoke();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public abstract Task GetTask();

    /// <summary>
    /// Completed the task, and sends a message to CheckListProducers to update their info. 
    /// </summary>
    public void CompleteTask() {
        if(GetTask().IsComplete()) {
            this.taskUpdateCall.Invoke();
        }
    }

    public string GetTitle() {
        return GetTask().GetTaskTitle();
    }

    /// <summary>
    /// Sets the checkbox.
    /// </summary>
    /// <param name="checkbox">the checkbox to set it as</param>
    public void SetCheckbox(Toggle checkbox)
    {
        this.checkbox = checkbox;
    }

    public Toggle GetCheckbox()
    {
        return this.checkbox;
    }

    public bool IsCompleted() {
        return GetTask().IsComplete();
    }

    public UnityEvent GetUpdateCall() {
        return this.taskUpdateCall;
    }

    /// <summary>
    /// Is thrown when the task is updated
    /// </summary>
    /// <param name="completed"></param>
    public delegate void OnGetUpdateCallDelegate(bool completed);

    public event OnGetUpdateCallDelegate OnTaskCompleted;
}