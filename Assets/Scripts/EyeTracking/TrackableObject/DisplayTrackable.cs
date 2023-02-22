using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Displays a trackable object.
/// </summary>
[RequireComponent(typeof(TrackableObjectController))]
public class DisplayTrackable : MonoBehaviour, TrackableObserver
{
    [SerializeField]
    private TextMeshPro fixationsText;

    [SerializeField]
    private TextMeshPro fixationDurationText;

    [SerializeField]
    private TextMeshPro averageFixationDurationText;

    private bool activeText = true;

    // Start is called before the first frame update
    void Start(){
        gameObject.GetComponent<TrackableObjectController>().AddObserver(this);
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

    /// </inheritdoc>
    public void UpdateAverageFixationDuration(float averageFixationDuration){
        averageFixationDurationText.text = Math.Round(averageFixationDuration, 1).ToString();
        averageFixationDurationText.gameObject.SetActive(true);
    }

    /// </inheritdoc>
    public void UpdateFixationDuration(float fixationDuration){
        fixationDurationText.text = Math.Round(fixationDuration, 1).ToString();
    }

    /// </inheritdoc>
    public void UpdateFixations(int fixations){
       fixationsText.text = fixations.ToString();
    }

    /// <summary>
    /// Toggles the text between being visible and not
    /// </summary>
    public void ToggleVisibleStats() {
        activeText = !activeText;
        fixationsText.gameObject.SetActive(activeText);
        fixationDurationText.gameObject.SetActive(activeText);
        averageFixationDurationText.gameObject.SetActive(activeText);
    }
}
