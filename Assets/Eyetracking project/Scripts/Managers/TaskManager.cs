using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Fetches and orders lists of objects with the task component. 
/// </summary>
public class TaskManager : MonoBehaviour {
    
    [SerializeField, Tooltip("A list of objects with the Task component.")]
    private List<TaskController> liveTasks;

    private static TaskManager taskManager;

    [SerializeField, Tooltip("The task configurations.")]
    private List<TaskConfiguration> taskConfigurations = new List<TaskConfiguration>();

    [SerializeField, Tooltip("Forces the task order")]
    private bool forceTaskOrder;

    [SerializeField, Tooltip("The current task")]
    private int currentTask = 1;
    
    private void Awake()
    {
        if (taskManager != null)
        {
            Destroy(this);
        }
        else
        {
            taskManager = this;
            DeployTasks();
        }
    }

    /// <summary>
    /// Deploys and builds all the liveTasks.
    /// </summary>
    public void DeployTasks() {
        Dictionary<int, ObjectTaskBuilder> taskBuilderMap = new Dictionary<int, ObjectTaskBuilder>();
        int order = 1;
        foreach (TaskConfiguration gazeConfig in taskConfigurations) {
            GameObject taskObject = gazeConfig.GetGameObject();
            ObjectTaskBuilder objectTaskBuilder = new ObjectTaskBuilder(taskObject, forceTaskOrder);
            if (objectTaskBuilder.AddLookGazeTask(gazeConfig.GetLookAtTask(), order))
            {
                order++;
            }
            if (objectTaskBuilder.AddHoldGazeTask(gazeConfig.GetGazePeriodTask(), order)) { 
                order++;  
            }
            XRGrabInteractable grabInteractable = taskObject.GetComponent<XRGrabInteractable>();
            if (grabInteractable != null) {
                if (objectTaskBuilder.AddTouchTask(gazeConfig.GetTouchObjectTask(), order)) {
                    order++;
                }
                if (objectTaskBuilder.AddHoldObjectForXTimeTask(gazeConfig.GetHoldObjectTask(), order)) {
                    order++;
                }
            }
            taskBuilderMap.Add(taskObject.GetInstanceID(), objectTaskBuilder);
        }

        foreach (ObjectTaskBuilder builder in taskBuilderMap.Values) {
            GameObject gameobject = builder.Build();
            liveTasks.AddRange(gameobject.GetComponents<TaskController>());
        }
        UpdateTaskList();
    }

    /// <summary>
    /// Gets the task manager.
    /// </summary>
    /// <returns>the task manager</returns>
    public static TaskManager GetTaskManager() {
        return taskManager;
    }

    /// <summary>
    /// Gets the current task.
    /// </summary>
    /// <returns>the current task.</returns>
    public int GetCurrentTask() {
        return currentTask;
    }

    /// <summary>
    /// Gets the task list.
    /// </summary>
    /// <returns>gets the task list</returns>
    public List<TaskController> GetTaskList() {
        return liveTasks;
    }

    /// <summary>
    /// Returns the amount of liveTasks that are not yet completed
    /// </summary>
    /// <returns>The amount of remaining liveTasks as an integer</returns>
    public int GetRemainingTaskAmount()
    {
        int remainingTaskAmount = 0;
        foreach (TaskController  task in liveTasks) {
            if(!task.IsCompleted()) {
                remainingTaskAmount++;
            }
        }
        return remainingTaskAmount;
    }

    /// <summary>
    /// Removes all completed liveTasks from the list of liveTasks
    /// </summary>
    public void PruneCompletedTasks() {
        foreach (TaskController task in liveTasks) {
            int i = 0;
            if(task.IsCompleted()) {
                liveTasks.Remove(task);
            }
            i++;
        }
    }

    /// <summary>
    /// Sorts the list with completed liveTasks on the bottom
    /// </summary>
    public void SendCompletedTasksToEndOfList() {
        List<TaskController> sortedTasks = new List<TaskController>();
        foreach (TaskController task in liveTasks) {
            if (!task.IsCompleted()) {
                sortedTasks.Add(task);
            }
        }

        foreach (TaskController task in liveTasks) {
            if(task.IsCompleted()) {
                sortedTasks.Add(task);
            }
        }
        liveTasks = sortedTasks;
    }

    private void UpdateTaskStatus() {
        currentTask += 1;
        SendCompletedTasksToEndOfList();
    }

    /// <summary>
    /// Updates the list with all objects containing the task component in the current scene. 
    /// </summary>
    private void UpdateTaskList() {
        foreach(TaskController task in liveTasks) {
            task.GetUpdateCall().AddListener(UpdateTaskStatus);
        }
    }
}