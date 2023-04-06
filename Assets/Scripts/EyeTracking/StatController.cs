using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatController : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro fixationsText;

    [SerializeField]
    private TextMeshPro fixationDurationText;

    [SerializeField]
    private TextMeshPro averageFixationDurationText;

    [SerializeField]
    private bool activeText = true;

    private void Start()
    {
        ToggleVisibleStats();
        CheckField("Fixations text", fixationsText);
        CheckField("Fixations durations text", fixationDurationText);
        CheckField("Average fixation duration text", averageFixationDurationText);
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
    /// Sets the amount of fixations.
    /// </summary>
    /// <param name="fixationsText">the fixations textMap</param>
    public void SetFixationsText(string fixationsText) { 
        this.fixationsText.text = fixationsText;
    }

    /// <summary>
    /// Sets the fixation duration textMap.
    /// </summary>
    /// <param name="fixationDuration">the fixation duration textMap</param>
    public void SetFixationDurationText(string fixationDuration) {
        this.fixationDurationText.text = fixationDuration;
    }

    /// <summary>
    /// Sets the average fixation druation textMap.
    /// </summary>
    /// <param name="averageFixationDuration">the average fixation duration</param>
    public void SetAverageFixationDurationText(string averageFixationDuration) {
        this.averageFixationDurationText.text = averageFixationDuration;
        averageFixationDurationText.gameObject.SetActive(true);
    }

    /// <summary>
    /// Toggles the textMap between being visible and not
    /// </summary>
    public void ToggleVisibleStats()
    {
        activeText = !activeText;
        fixationsText.gameObject.SetActive(activeText);
        fixationDurationText.gameObject.SetActive(activeText);
        averageFixationDurationText.gameObject.SetActive(activeText);
    }

    /// <summary>
    /// Turns the textMap towards the current player.
    /// </summary>
    /// <param name="playerTransform">the current player</param>
    public void TurnText(Transform playerTransform) {
        CheckIfObjectIsNull(playerTransform, "playertransform");
        this.transform.LookAt(playerTransform);
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
