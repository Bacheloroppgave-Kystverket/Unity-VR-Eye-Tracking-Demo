using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A task that needs to be performed by the user. Contains a boolean that turns true when the task conditions are met.
/// </summary>
public class Task : MonoBehaviour {
    [SerializeField]
    private string taskTitle;
    private bool isCompleted;
    
    enum TaskType {
        GoToPosition,
        PickUpObject,
        EvaluateObject
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CompleteTask() {
        isCompleted = true;
    }

    public string GetTitle() {
        return taskTitle;
    }
}
