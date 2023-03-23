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
    private StatController statController;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private bool deployPrefab;

    [SerializeField, Tooltip("Set to true if the object has a predefined stat controller")]
    private bool checkStatController = true;


    // Start is called before the first frame update
    void Start(){
        gameObject.GetComponent<TrackableObjectController>().AddObserver(this);
        CheckField("prefab", prefab);
        if (checkStatController) {
            CheckField("stat controller", statController);
        }
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
        
        statController.SetAverageFixationDurationText(Math.Round(averageFixationDuration, 1).ToString());        
    }

    /// </inheritdoc>
    public void UpdateFixationDuration(float fixationDuration){
        statController.SetFixationDurationText(Math.Round(fixationDuration, 1).ToString());
    }

    /// </inheritdoc>
    public void UpdateFixations(int fixations) {
        statController.SetFixationsText(fixations.ToString());
    }

    /// <summary>
    /// Toggles the text between being visible and not
    /// </summary>
    public void ToggleVisibleStats()
    {
        statController.ToggleVisibleStats();
    }
}
