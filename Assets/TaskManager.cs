using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Fetches and orders lists of objects with the task component. 
/// </summary>
public class TaskManager : MonoBehaviour {
    [SerializeField] [Tooltip("A list of objects with the Task component.")]
    private List<Task> tasks;

    private void Start() {
        UpdateTaskList();
    }

    public List<Task> GetTaskList() {
        return tasks;
    }

    /// <summary>
    /// Returns the amount of tasks that are not yet completed
    /// </summary>
    /// <returns>The amount of remaining tasks as an integer</returns>
    public int GetRemainingTaskAmount()
    {
        int remainingTaskAmount = 0;
        foreach (Task  task in tasks) {
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
        foreach (Task task in tasks) {
            int i = 0;
            if(task.IsCompleted()) {
                tasks.Remove(task);
            }
            i++;
        }
    }

    /// <summary>
    /// TODO: Functionality
    /// </summary>
    public void SendCompletedTasksToEndOfList()
    {
        foreach (Task task in tasks)
        {
            int i = 0;
            if (task.IsCompleted())
            {
                tasks.Remove(task);
            }
            i++;
        }
    }

    /// <summary>
    /// Updates the list with all objects containing the task component in the current scene. 
    /// </summary>
    public void UpdateTaskList() {
        tasks = GameObject.FindObjectsOfType<Task>().ToList();
        foreach(Task task in tasks) {
            task.GetUpdateCall().AddListener(UpdateTaskList);
        }
    }
}