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
        averageFixationDurationText.gameObject.SetActive(false);
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
    /// <param name="fixationsText">the fixations text</param>
    public void SetFixationsText(string fixationsText) { 
        this.fixationsText.text = fixationsText;
    }

    /// <summary>
    /// Sets the fixation duration text.
    /// </summary>
    /// <param name="fixationDuration">the fixation duration text</param>
    public void SetFixationDurationText(string fixationDuration) {
        this.fixationDurationText.text = fixationDuration;
    }

    /// <summary>
    /// Sets the average fixation druation text.
    /// </summary>
    /// <param name="averageFixationDuration">the average fixation duration</param>
    public void SetAverageFixationDurationText(string averageFixationDuration) {
        this.averageFixationDurationText.text = averageFixationDuration;
        averageFixationDurationText.gameObject.SetActive(true);
    }

    /// <summary>
    /// Toggles the text between being visible and not
    /// </summary>
    public void ToggleVisibleStats()
    {
        activeText = !activeText;
        fixationsText.gameObject.SetActive(activeText);
        fixationDurationText.gameObject.SetActive(activeText);
        averageFixationDurationText.gameObject.SetActive(activeText);
    }

}
