using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This manager has a responsebility to handle the feedback that the user gets through the adaptive training system.
/// </summary>
public class FeedbackManager : MonoBehaviour
{

    [SerializeField, Tooltip("The trackable objects manager")]
    private TrackableObjectsManager trackableObjectsManager;

    [SerializeField, Tooltip("The overlay manager")]
    private OverlayManager overlayManager;

    [SerializeField, Tooltip("The configuration for the objects")]
    private List<FeedbackConfiguration> feedbackConfigurations = new List<FeedbackConfiguration>();

    [SerializeField, Tooltip("The feedback story")]
    private List<Feedback> feedbackStory = new List<Feedback>();

    // Start is called before the first frame update
    void Start()
    {
        /*
        GameObject[] objects = GameObject.FindGameObjectsWithTag(typeof(TrackableObject).Name);
        foreach (GameObject gameObject in objects) {
            TrackableObject trackableObject = gameObject.GetComponent<TrackableObject>();
            if (trackableObject != null && !feedbackConfigurations.Exists(feedback => feedback.GetTrackableObject().GetInstanceID() == trackableObject.GetInstanceID())) {
                feedbackConfigurations.Add(new FeedbackConfiguration(trackableObject));
            }
        }
        */
        float prosentage = feedbackConfigurations.Sum(feedback => feedback.GetThreshold());
        CheckIfListIsValid("Feedbacks", feedbackConfigurations.Any());
        CheckField("Trackable objects manager", trackableObjectsManager);
        CheckField("Overlay manager", overlayManager);
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
    /// Checks if the list has any elements.
    /// </summary>
    /// <param name="error">the error to display</param>
    /// <param name="hasAny">true if the list has any. False otherwise</param>
    private void CheckIfListIsValid(string error, bool hasAny) {
        if (!hasAny) {
            Debug.Log("<color=red>Error:</color>" + error + " cannot be emtpy", gameObject);
        }
    }

    public void CalculateAndDisplayFeedback() {
        Dictionary<TrackableObject, float> prosentageMap = trackableObjectsManager.CalculateProsentageWatchedForSeat();
        Feedback feedback = new Feedback(prosentageMap);
        feedbackStory.Add(feedback);
        overlayManager.DisplayFeedback(feedback);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
