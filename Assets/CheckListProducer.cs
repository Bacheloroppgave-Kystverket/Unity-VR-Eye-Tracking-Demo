using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CheckListProducer : MonoBehaviour
{
    [SerializeField]
    private GameObject checkboxPrefab;
    private GameObject[] checkboxList;
    [SerializeField]
    private TextMeshProUGUI amountRemainingDisplay;
    [SerializeField]
    private TaskManager taskManager;
    private Task[] tasks;

    // Start is called before the first frame update
    void Start()
    {
        tasks = taskManager.GetTaskList();
        foreach (Task task in tasks) {
            GameObject taskBox = Instantiate(checkboxPrefab, gameObject.transform);
            task.SetCheckbox(taskBox);
            taskBox.transform.parent = gameObject.transform;
            Text textField = taskBox.GetComponentInChildren<Text>();
            textField.text = task.GetTitle();
            amountRemainingDisplay.text = "Tasks left: " + taskManager.GetRemainingTaskAmount();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void UpdateList()
    {
        foreach (Task task in tasks) {
            if(task.IsCompleted())
            {
                
            }
        }
    }
}
