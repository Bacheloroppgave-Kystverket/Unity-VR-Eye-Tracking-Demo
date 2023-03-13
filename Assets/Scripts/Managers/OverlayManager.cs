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
    public void DisplayFeedback(AdaptiveFeedback feedback) {
        feedbackText.text = feedback.GetFeedback();
    }

    /// <summary>
    /// Displays the least viewed object to the user based on their threshold.
    /// </summary>
    /// <param name="adaptiveFeedback">the adaptive feedback</param>
    /// <param name="referencePosition">the reference position</param>
   public void DisplayLeastViewedObject(AdaptiveFeedback adaptiveFeedback, ReferencePosition referencePosition) {
        feedbackText.text = adaptiveFeedback.GetLeastViewedObjectAsString(referencePosition.GetAllFeedbackConfigurations());
    }

    /// <inheritdoc/>
    public void UpdateFeedback(Feedback feedback)
    {
        if (feedback is AdaptiveFeedback feed)
        {
            DisplayFeedback(feed);
        }
        else {
            MonoBehaviour.print("Feedback type of " + feedback.GetType() + " is not implemented");
        }
    }
}
