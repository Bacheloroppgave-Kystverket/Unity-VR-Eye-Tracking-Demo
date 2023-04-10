using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// A task that needs to be performed by the user. Contains a boolean that turns true when the task conditions are met.
/// </summary>
public class Task : MonoBehaviour {
    [SerializeField]
    private string taskTitle;
    private GameObject checkbox;
    private bool isCompleted;
    private UnityEvent taskCompleted = new UnityEvent();

    /// <summary>
    /// Completed the task, and sends a message to CheckListProducers to update their info. 
    /// </summary>
    public void CompleteTask() {
        isCompleted = true;
        SendMessage("UpdateList");
        checkbox.GetComponent<Toggle>().isOn = true;
        taskCompleted.Invoke();
    }

    public string GetTitle() {
        return taskTitle;
    }

    public void SetCheckbox(GameObject checkbox)
    {
        this.checkbox = checkbox;
    }

    public GameObject GetCheckbox()
    {
        return this.checkbox;
    }

    public bool IsCompleted()
    {
        return isCompleted;
    }

    public UnityEvent GetCompletedEvent() {
        return this.taskCompleted;
    }

    /// <summary>
    /// Is thrown when the task is completed
    /// </summary>
    /// <param name="completed"></param>
    public delegate void OnTaskCompletedDelegate(bool completed);
    public event OnTaskCompletedDelegate OnTaskCompleted;
}
