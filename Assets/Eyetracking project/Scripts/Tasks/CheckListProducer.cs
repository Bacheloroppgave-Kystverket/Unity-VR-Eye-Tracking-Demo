

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Produces and maintains UI checklists for TaskManagers
/// </summary>
public class CheckListProducer : MonoBehaviour {

    [SerializeField]
    [Tooltip("The prefab that will function as a checkbox")]
    private Toggle checkboxPrefab;

    [SerializeField]
    [Tooltip("The text field that will display the amount of tasks remaining")]
    private TextMeshProUGUI amountRemainingDisplay;

    [SerializeField]
    [Tooltip("The TaskManager the CheckListProducer will base its checklist on")]
    private TaskManager taskManager;

    private List<TaskController> tasks;

    /// <summary>
    /// Initializes connection with the taskmanager, and initializes the list
    /// </summary>
    void Start() {
        this.taskManager = TaskManager.GetTaskManager();
        InitializeChecklist();
    }

    /// <summary>
    /// Initializes the checklist producer.
    /// </summary>
    public void InitializeChecklist() {
        tasks = taskManager.GetTaskList();
        foreach (TaskController task in tasks) {
            task.GetUpdateCall().AddListener(UpdateList);
        }
        UpdateList();
    }

    /// <summary>
    /// Updates the list so that it complies with the TaskManager's list. Completed liveTasks are checked off, new uncompleted liveTasks are added to the list. 
    /// </summary>
    public void UpdateList()
    {
        tasks = taskManager.GetTaskList();
        foreach (TaskController task in tasks) {
            Toggle toggle = task.GetCheckbox();
            if (toggle == null) {
                toggle = Instantiate(checkboxPrefab, gameObject.transform);
                task.SetCheckbox(toggle);
            }
            Text textField = toggle.GetComponentInChildren<Text>();
            textField.text = task.GetTitle();
            if (task.IsCompleted()) {
                ColourizeText(textField);
                toggle.isOn = true;
                toggle.transform.SetAsLastSibling();
            }
        }
        UpdateRemainingTaskAmount();
    }

    /// <summary>
    /// Updates the textfield displaying the amount of uncompleted liveTasks.
    /// </summary>
    private void UpdateRemainingTaskAmount() {
        amountRemainingDisplay.text = "Tasks left: " + taskManager.GetRemainingTaskAmount();
    }
    
    /// <summary>
    /// Turns the selected textelement into a shade of green. TODO: Make it possible to change into any colour
    /// </summary>
    /// <param name="textElement"></param>
    private void ColourizeText(Text textElement) {
        textElement.color = new Color(0.341f, 0.612f, 0.29f);
    }
}
