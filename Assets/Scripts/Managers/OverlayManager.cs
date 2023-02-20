using TMPro;
using UnityEngine;

public class OverlayManager : MonoBehaviour, SessionObserver
{
    [SerializeField]
    private TextMeshProUGUI feedbackText;

    private void Start()
    {
        CheckField("Feedback text", feedbackText);
    }

    /// <summary>
    /// Checks if the defined field is set in the editor.
    /// </summary>
    /// <param name="error">the type of error like "type of object"</param>
    /// <param name="fieldToCheck">The field to check</param>
    private bool CheckField(string error, object fieldToCheck)
    {
        bool valid = fieldToCheck == null;
        if (valid)
        {
            Debug.Log("<color=red>Error:</color>" + error + " must be set.", gameObject);
        }
        return valid;
    }

    /// <summary>
    /// Displays the feedback to the user.
    /// </summary>
    /// <param name="feedback">the feedback</param>
    public void DisplayFeedback(ProsentageTypeFeedback feedback) {
        feedbackText.text = feedback.GetFeedback();
    }

    /// <inheritdoc/>
    public void UpdateFeedback(Feedback feedback)
    {
        if (feedback is ProsentageTypeFeedback feed)
        {
            DisplayFeedback(feed);
        }
        else {
            MonoBehaviour.print("Feedback type of " + feedback.GetType() + " is not implemented");
        }
    }
}
