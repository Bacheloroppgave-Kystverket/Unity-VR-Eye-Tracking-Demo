using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// Produces and maintains UI checklists for TaskManagers
/// </summary>
public class CheckListProducer : MonoBehaviour {
    [SerializeField]
    [Tooltip("The prefab that will function as a checkbox")]
    private GameObject checkboxPrefab;

    [SerializeField]
    [Tooltip("The text field that will display the amount of tasks remaining")]
    private TextMeshProUGUI amountRemainingDisplay;

    [SerializeField]
    [Tooltip("The TaskManager the CheckListProducer will base its checklist on")]
    private TaskManager taskManager;

    private GameObject[] checkboxList;
   
    private List<Task> tasks;

    // Start is called before the first frame update
    void Start() {
        UpdateList();
    }

    void UpdateList()
    {
        tasks = taskManager.GetTaskList();
        foreach (Task task in tasks) {
            if (task.GetCheckbox() == null) {
                GameObject taskBox = Instantiate(checkboxPrefab, gameObject.transform);
                task.SetCheckbox(taskBox);
                taskBox.transform.parent = gameObject.transform;
                Text textField = taskBox.GetComponentInChildren<Text>();
                textField.text = task.GetTitle();
                task.GetCompletedEvent().AddListener(UpdateList);
            }
        }
        UpdateRemainingTaskAmount();
    }

    void UpdateRemainingTaskAmount() {
        amountRemainingDisplay.text = "Tasks left: " + taskManager.GetRemainingTaskAmount();
    }
}
