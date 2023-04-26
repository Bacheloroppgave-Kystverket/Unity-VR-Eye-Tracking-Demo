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
    [SerializeField]
    private UnityEvent taskUpdateCall = new UnityEvent();

    private void Start() {
        this.taskUpdateCall.Invoke();
    }

    private void OnEnable() {
        this.taskUpdateCall.Invoke();
    }

    private void OnDisable() {
        this.taskUpdateCall.Invoke();
    }

    /// <summary>
    /// Completed the task, and sends a message to CheckListProducers to update their info. 
    /// </summary>
    public void CompleteTask() {
        isCompleted = true;
        SendMessage("UpdateList");
        checkbox.GetComponent<Toggle>().isOn = true;
        this.taskUpdateCall.Invoke();
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

    public bool IsCompleted() {
        return isCompleted;
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