using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Represents a task that is a question.
/// </summary>
[Serializable]
public class QuestionTask : Task
{
    [SerializeField, Tooltip("The question options")]
    private List<QuestionOption> options = new List<QuestionOption>();

    ///<inheritdoc/>
    public override bool IsComplete()
    {
        return options.All(option => option.IsAwnseredCorrectly());
    }

    /// <summary>
    /// Gets the question options.
    /// </summary>
    /// <returns>the options as a list</returns>
    public List<QuestionOption> GetOptions() => options;

    ///<inheritdoc/>
    public override void ResetTask()
    {
        options.ForEach(option => option.SetChosen(false));
    }
}
