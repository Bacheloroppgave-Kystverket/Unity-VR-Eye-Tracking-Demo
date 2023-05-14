using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Controller of a dialog
/// </summary>
public class DialogController : MonoBehaviour
{
    [SerializeField, Tooltip("The main content")]
    private TextMeshProUGUI mainContent;

    [SerializeField, Tooltip("The title of the dialog")]
    private TextMeshProUGUI title;

    [SerializeField, Tooltip("The dialog")]
    private Dialog dialog;

    /// <summary>
    /// Sets the dialog of the dialog controller.
    /// </summary>
    /// <param name="dialog">the dialog</param>
    public void SetDialog(Dialog dialog) {
        CheckIfObjectIsNull(dialog, "dialog");
        this.dialog = dialog;
    }

    /// <summary>
    /// Shows the dialog.
    /// </summary>
    public void ShowDialog() {
        this.title.text = dialog.GetTitle();
        this.mainContent.text = dialog.GetMessage();
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides the dialog.
    /// </summary>
    public void HideDialog() { 
        gameObject.SetActive(false);
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
