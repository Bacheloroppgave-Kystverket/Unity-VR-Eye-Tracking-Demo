using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour {
    [SerializeField]
    private List<Task> tasks;

    private void Start() {
    }
    public List<Task> GetTaskList() {
        return tasks;
    }

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
}