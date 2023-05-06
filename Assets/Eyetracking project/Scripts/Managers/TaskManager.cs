using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Fetches and orders lists of objects with the task component. 
/// </summary>
public class TaskManager : MonoBehaviour {
    
    [SerializeField, Tooltip("A list of objects with the Task component.")]
    private List<TaskController> tasks;

    private static TaskManager taskManager;

    private void Awake()
    {
        if (taskManager != null)
        {
            Destroy(this);
        }
        else
        {
            taskManager = this;
        }
    }

    /// <summary>
    /// Gets the task manager.
    /// </summary>
    /// <returns>the task manager</returns>
    public static TaskManager GetTaskManager() {
        return taskManager;
    }

    /// <summary>
    /// Gets the task list.
    /// </summary>
    /// <returns>gets the task list</returns>
    public List<TaskController> GetTaskList() {
        if (tasks.Count == 0) {
            UpdateTaskList();
        }
        return tasks;
    }

    /// <summary>
    /// Returns the amount of tasks that are not yet completed
    /// </summary>
    /// <returns>The amount of remaining tasks as an integer</returns>
    public int GetRemainingTaskAmount()
    {
        int remainingTaskAmount = 0;
        foreach (TaskController  task in tasks) {
            if(!task.IsCompleted()) {
                remainingTaskAmount++;
            }
        }
        return remainingTaskAmount;
    }

    /// <summary>
    /// Removes all completed tasks from the list of tasks
    /// </summary>
    public void PruneCompletedTasks() {
        foreach (TaskController task in tasks) {
            int i = 0;
            if(task.IsCompleted()) {
                tasks.Remove(task);
            }
            i++;
        }
    }

    /// <summary>
    /// Sorts the list with completed tasks on the bottom
    /// </summary>
    public void SendCompletedTasksToEndOfList() {
        List<TaskController> sortedTasks = new List<TaskController>();
        foreach (TaskController task in tasks) {
            if (!task.IsCompleted()) {
                sortedTasks.Add(task);
            }
        }

        foreach (TaskController task in tasks) {
            if(task.IsCompleted()) {
                sortedTasks.Add(task);
            }
        }
        tasks = sortedTasks;
    }

    private void UpdateTaskStatus() {
        MonoBehaviour.print("Manager update");
        SendCompletedTasksToEndOfList();
    }

    /// <summary>
    /// Updates the list with all objects containing the task component in the current scene. 
    /// </summary>
    private void UpdateTaskList() {
        tasks = GameObject.FindObjectsOfType<TaskController>().ToList();
        
        foreach(TaskController task in tasks) {
            task.GetUpdateCall().AddListener(UpdateTaskStatus);
        }
    }
}