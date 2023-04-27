using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents an controller for the dev overlay.
/// </summary>
public class DevOverlayController : MonoBehaviour
{
    [Header("Configure controller")]
    [SerializeField, Tooltip("The feedback panel.")]
    private RectTransform feedbackPanel;

    [SerializeField, Tooltip("Shows the feedback")]
    private Toggle showFeedbackToggle;

    [SerializeField, Tooltip("The feedback text to change")]
    private TextMeshProUGUI feedbackText;

    [SerializeField, Tooltip("True if the feedback should be shown. False otherwise")]
    private bool showFeedback;

    private void Start()
    {
        SetShowFeedback(showFeedback);
    }

    /// <summary>
    /// Sets the show feedback to a new value.
    /// </summary>
    /// <param name="setShowFeedback">true if the feedback should be shown. False otherwise.</param>
    public void SetShowFeedback(bool setShowFeedback) {
        showFeedback = setShowFeedback;
        feedbackPanel.gameObject.SetActive(showFeedback);
    }

    /// <summary>
    /// Sets the feedback text.
    /// </summary>
    /// <param name="feedback">the feedback text</param>
    public void SetFeedback(string feedback) {
        CheckIfObjectIsNull(feedback, "feedback");
        feedbackText.text = feedback;
    }

    /// <summary>
    /// Checks if the object is null or not. Throws an exception if the object is null.
    /// </summary>
    /// <param name="objecToCheck">the object to check</param>
    /// <param name="error">the error to be in the string.</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the object to check is null.</exception>
    private void CheckIfObjectIsNull(object objecToCheck, string error)
    {
        if (objecToCheck == null)
        {
            throw new IllegalArgumentException("The " + error + " cannot be null.");
        }
    }
}
