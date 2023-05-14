using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a taks that is in the GUI. 
/// </summary>
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

    [SerializeField, Tooltip("The error text")]
    private TextMeshProUGUI errorText;

    [SerializeField, Tooltip("T")]
    private Button submitButton;

    [SerializeField, Tooltip("The question tasks")]
    private List<QuestionTask> questionTasks;

    [SerializeField, Tooltip("The current toggles")]
    private Dictionary<QuestionOption, Toggle> questionTogglesMap = new Dictionary<QuestionOption, Toggle>();

    [SerializeField, Tooltip("Set to true if the overlay should hide")]
    private bool hideOverlay;

    private int pos = -1;

    ///<inheritdoc/>
    public override Task GetTask()
    {
        return simpleTask;
    }

    private void Start()
    {
        DisplayCurrentTask();
        StartCoroutine(Hide());
    }

    /// <summary>
    /// Hides the GUI task controller.
    /// </summary>
    /// <returns>the enumerator</returns>
    private IEnumerator Hide() {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
        submitButton.onClick.AddListener(SubmitAwnser);
    }

    /// <summary>
    /// Called by the button in order to check if all the awnsers are correct.
    /// </summary>
    public void SubmitAwnser() {
        foreach(QuestionOption questionOption in questionTogglesMap.Keys){
            Toggle toggle = questionTogglesMap[questionOption];
            questionOption.SetChosen(toggle.isOn);
            MonoBehaviour.print("The question option is " + questionOption.IsAwnseredCorrectly() + " " + toggle.isOn);
        }
        MonoBehaviour.print(questionTasks[pos].IsComplete());

        if (pos < questionTasks.Count && questionTasks[pos].IsComplete())
        {
            StartCoroutine(DisplayCorrectAnswer());
        }
        else {
            StartCoroutine(ShowErrorTextAndClearOptions());
        }
    }

    /// <summary>
    /// Displays that the anwser was correct and goes to the next task.
    /// </summary>
    /// <returns>the enumerator</returns>
    public IEnumerator DisplayCorrectAnswer() {
        DestoryTogglesAndClearTasks();
        submitButton.interactable = false;
        questionTitle.text = "Correct answer!";
        yield return new WaitForSeconds(2);
        submitButton.interactable = true;
        DisplayCurrentTask();
    }

    /// <summary>
    /// Starts a corerutine to display an error and disable the submit button.
    /// </summary>
    /// <returns>Enumerator</returns>
    public IEnumerator ShowErrorTextAndClearOptions() {
        submitButton.interactable = false;
        foreach (QuestionOption questionOption in questionTogglesMap.Keys) {
            questionTogglesMap[questionOption].isOn = false;
        }
        errorText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        errorText.gameObject.SetActive(false);
        submitButton.interactable = true;
    }

    /// <summary>
    /// Displays the current task.
    /// </summary>
    private void DisplayCurrentTask() {
        pos += 1;
        if (pos < questionTasks.Count && questionTasks.Count > 0)
        {
            QuestionTask task = questionTasks[pos];
            this.questionTitle.text = task.GetTaskTitle();
            GenerateTogglesForOptions(task);
            submitButton.gameObject.SetActive(true);
        }
        else {
            questionTitle.text = "All tasks are done";
            submitButton.gameObject.SetActive(false);
            simpleTask.SetDone();
            CompleteTask();
        }

    }

    /// <summary>
    /// Generates toggles for each of the options.
    /// </summary>
    /// <param name="task">the task</param>
    private void GenerateTogglesForOptions(QuestionTask task) {
        task.GetOptions().ForEach(option =>
        {
            Toggle toggle = Instantiate(togglePrefab, taskHolder.transform);
            toggle.GetComponentInChildren<Text>().text = option.GetQuestionOptionAsText();
            questionTogglesMap.Add(option, toggle);
        });

    }

    /// <summary>
    /// Destroys the toggles and clears the question and toggles map.
    /// </summary>
    private void DestoryTogglesAndClearTasks() {
        foreach(QuestionOption questionOption in questionTogglesMap.Keys) {
            Toggle toggle = questionTogglesMap[questionOption];
            Destroy(toggle.gameObject);
        }
        questionTogglesMap.Clear();
    }
}
