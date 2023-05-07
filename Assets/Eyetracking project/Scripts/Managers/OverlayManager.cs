using TMPro;
using UnityEngine;

public class OverlayManager : MonoBehaviour
{
    [SerializeField, Tooltip("The feedback text in VR")]
    private TextMeshProUGUI feedbackText;

    [SerializeField, Tooltip("The dev overlay controller")]
    private DevOverlayController controller;

    [SerializeField, Tooltip("The menus amount text")]
    private TextMeshProUGUI menuAmountText;

    [SerializeField, Tooltip("The dialog controller")]
    private DialogController dialogController;

    private int feedbackAmount;

    private void Start()
    {
        CheckField("Feedback text", feedbackText);
        CheckField("Dev controller", controller);
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
        if (feedbackText != null) {
            feedbackAmount += 1;
            feedbackText.text = feedback.GetFeedback();
            controller.SetFeedback(feedback.GetFeedback());
            menuAmountText.text = "Amount: " + feedbackAmount.ToString();
        }
    }


    /// <summary>
    /// Displays the least viewed object to the user based on their threshold.
    /// </summary>
    /// <param name="adaptiveFeedback">the adaptive feedback</param>
    /// <param name="referencePosition">the reference position</param>
   public void DisplayLeastViewedObject(AdaptiveFeedback adaptiveFeedback, ReferencePosition referencePosition) {
        feedbackText.text = adaptiveFeedback.GetLeastViewedObjectAsString(referencePosition.GetCategoryConfigurationsForPosition());
    }

    /// <summary>
    /// Shows the dialog to the user.
    /// </summary>
    /// <param name="dialog">the dialog</param>
    public void ShowDialog(Dialog dialog) {
        dialogController.SetDialog(dialog);
        dialogController.ShowDialog();
    }
}
