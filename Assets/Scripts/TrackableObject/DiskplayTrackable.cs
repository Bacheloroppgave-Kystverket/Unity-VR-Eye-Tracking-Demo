using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Displays a trackable object.
/// </summary>
[RequireComponent(typeof(TrackableObject))]
public class DiskplayTrackable : MonoBehaviour, TrackableObserver
{
    [SerializeField]
    private TextMeshPro fixationsText;

    [SerializeField]
    private TextMeshPro fixationDurationText;

    [SerializeField]
    private TextMeshPro averageFixationDurationText;

    // Start is called before the first frame update
    void Start(){
        gameObject.GetComponent<TrackableObject>().AddObserver(this);
        averageFixationDurationText.gameObject.SetActive(false);
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
}
