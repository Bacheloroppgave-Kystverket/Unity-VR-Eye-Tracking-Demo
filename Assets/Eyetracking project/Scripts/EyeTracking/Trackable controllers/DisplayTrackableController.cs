using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Displays a trackable object.
/// </summary>
[RequireComponent(typeof(TrackableObjectController))]
public class DisplayTrackableController : MonoBehaviour, TrackableObserver
{
    [Header("Configure object")]
    [SerializeField, Tooltip("The current stat controller. Can be set to null if the stats should auto-deploy.")]
    private StatController statController;

    [SerializeField, Tooltip("The prefab for the stats text.")]
    private GameObject prefab;

    [SerializeField, Tooltip("Set to true if the text is supposed to be on the right side.")]
    private bool rightSide;

    [Header("Other fields")]
    [SerializeField, Tooltip("The objects renderer")]
    private Renderer objectRenderer;

    private RectTransform rectTransform;


    // Start is called before the first frame update
    void Start(){
        gameObject.GetComponent<TrackableObjectController>().AddObserver(this);
        CheckField("prefab", prefab);
        this.objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null) {
            this.rectTransform = GetComponent<RectTransform>();
        }
        if (objectRenderer == null && rectTransform == null) {
            Debug.Log("A renderer for the object is needed. Either a rect transform or object renderer.");
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
        if (statController == null) {
            statController.SetAverageFixationDurationText(Math.Round(averageFixationDuration, 1).ToString());
        }
             
    }

    /// </inheritdoc>
    public void UpdateFixationDuration(float fixationDuration){
        if (statController != null)
        {
            statController.SetFixationDurationText(Math.Round(fixationDuration, 1).ToString());
        }
        
    }

    /// </inheritdoc>
    public void UpdateFixations(int fixations) {
        if (statController != null)
        {
            statController.SetFixationsText(fixations.ToString());
        }
        
    }

    /// <summary>
    /// Toggles the textMap between being visible and not
    /// </summary>
    /// <param name="playerTransform">the player transform</param>
    public void ToggleVisibleStats(Transform playerTransform)
    {
        if (statController == null && (objectRenderer != null || rectTransform != null))
        {
            statController = Instantiate(prefab).GetComponent<StatController>();
            statController.transform.SetParent(transform, false);
            statController.transform.position = transform.position + new Vector3(0, 0,-0.5f);
            float xScale = objectRenderer == null ? rectTransform.rect.width : objectRenderer.bounds.size.x;
            Vector3 newPos = new Vector3(); //transform.localPosition;
            //statController.transform.localScale.Set(1 / transform.localScale.x, 1 / transform.localScale.y, 1 / transform.localScale.z);
            if(rightSide){
                //statController.transform.localPosition = (newPos + new Vector3((xScale / 2), 1f, 0.5f));
            }else{
                //statController.transform.localPosition = (newPos + new Vector3((xScale / 2), 1f, 0.5f));
            }
        }
        if (statController != null) {
            statController.ToggleVisibleStats();
            statController.TurnText(playerTransform);
        }
    }
}
