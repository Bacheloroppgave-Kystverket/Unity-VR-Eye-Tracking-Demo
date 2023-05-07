using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUITaskController : TaskController
{

    [Space(5), Header("Task controller attributes")]
    [SerializeField, Tooltip("The task of this GUI")]
    private SimpleTask simpleTask;

    [SerializeField, Tooltip("The prefab for the toggle button.")]
    private Toggle togglePrefab;

    [SerializeField, Tooltip("The task holder area")]
    private GameObject taskHolder;

    [SerializeField, Tooltip("The question title")]
    private TextMeshProUGUI questionTitle;

    [SerializeField, Tooltip("The question tasks")]
    private List<QuestionTask> questionTasks;

    [SerializeField, Tooltip("The current toggles")]
    private Dictionary<QuestionTask, Toggle> questionTogglesMap = new Dictionary<QuestionTask, Toggle>();

    private int pos = 0;

    protected override Task GetTask()
    {
        return simpleTask;
    }

    public void DisplayCurrentTask() {
        if (pos < questionTasks.Count)
        {
            QuestionTask task = questionTasks[pos];
            this.questionTitle.text = task.GetTaskTitle();

            pos += 1;
        }
        else { 
        
        }

    }

    public void GenerateTogglesForOptions(Task task) {
        

    }

    public void DestoryTogglesAndClearTasks() { 
    
    }
}
