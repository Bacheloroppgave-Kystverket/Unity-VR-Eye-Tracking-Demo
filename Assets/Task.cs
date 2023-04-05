using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A task that needs to be performed by the user. Contains a boolean that turns true when the task conditions are met.
/// </summary>
public class Task : MonoBehaviour {
    [SerializeField]
    private string taskTitle;
    private GameObject checkbox;
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

    public void CompleteTask() {
        isCompleted = true;
        checkbox.GetComponent<Toggle>().isOn = true;
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
}
