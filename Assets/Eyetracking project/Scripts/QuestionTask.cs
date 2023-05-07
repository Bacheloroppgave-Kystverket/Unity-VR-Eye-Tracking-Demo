using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestionTask : Task
{
    [SerializeField, Tooltip("The question options")]
    private List<QuestionOption> options = new List<QuestionOption>();

    ///<inheritdoc/>
    public override bool IsComplete()
    {
        return options.All(option => option.IsAwnseredCorrectly());
    }

    ///<inheritdoc/>
    public override void ResetTask()
    {
        options.ForEach(option => option.SetChosen(false));
    }
}
