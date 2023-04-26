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
    private List<Task> tasks;

    // Start is called before the first frame update
    void Start() {
        UpdateList();
    }

    /// <summary>
    /// Updates the list so that it complies with the TaskManager's list. Completed tasks are checked off, new uncompleted tasks are added to the list. 
    /// </summary>
    public void UpdateList()
    {
        tasks = taskManager.GetTaskList();
        foreach (Task task in tasks) {
            Destroy(task.GetCheckbox());
            task.SetCheckbox(null);
            GameObject taskBox = Instantiate(checkboxPrefab, gameObject.transform);
            task.SetCheckbox(taskBox);
            taskBox.transform.parent = gameObject.transform;    Text textField = taskBox.GetComponentInChildren<Text>();
            textField.text = task.GetTitle();
            if (task.IsCompleted()) {
                ColourizeText(textField);
                task.GetCheckbox().GetComponent<UnityEngine.UI.Toggle>().isOn = true;
            }
            task.GetUpdateCall().AddListener(UpdateList);
        }
        UpdateRemainingTaskAmount();
    }

    /// <summary>
    /// Updates the textfield displaying the amount of uncompleted tasks.
    /// </summary>
    void UpdateRemainingTaskAmount() {
        amountRemainingDisplay.text = "Tasks left: " + taskManager.GetRemainingTaskAmount();
    }

    void ColourizeText(Text textElement) {
        textElement.color = new Color(0.341f, 0.612f, 0.29f);
    }
}
