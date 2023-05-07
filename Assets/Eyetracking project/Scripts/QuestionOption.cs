using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a option that might be selected under the question task.
/// </summary>
public class QuestionOption{

    [SerializeField, Tooltip("The question option")]
    private string questionOption;

    [SerializeField, Tooltip("Set to true if this question opinion is corrrect")]
    private bool correct;

    [SerializeField, Tooltip("Set to true if this question option was chosen. False otherwise")]
    private bool chosen;

    /// <summary>
    /// Gets the question option.
    /// </summary>
    /// <returns>the question option.</returns>
    public string GetQuestionOptionAsText() { 
        return questionOption;
    }

    /// <summary>
    /// Sets the chosen value of the task.
    /// </summary>
    /// <param name="chosenOption">the chosen option</param>
    public void SetChosen(bool chosenOption) { 
        this.chosen = chosenOption;
    }

    /// <summary>
    /// Checks if this question is awnsered correctly.
    /// </summary>
    /// <returns></returns>
    public bool IsAwnseredCorrectly() {
        return correct == chosen;
    }
}
