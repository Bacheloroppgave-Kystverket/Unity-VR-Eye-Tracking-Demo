using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This manager has a responsebility to handle the feedback that the user gets through the adaptive training system.
/// </summary>
public class FeedbackManager : MonoBehaviour
{

    [SerializeField]
    private SessionManager sessionManager;

    [SerializeField]
    private List<Feedback> feedbacks = new List<Feedback>();

    // Start is called before the first frame update
    void Start()
    {
        /*
        GameObject[] objects = GameObject.FindGameObjectsWithTag(typeof(TrackableObject).Name);
        foreach (GameObject gameObject in objects) {
            TrackableObject trackableObject = gameObject.GetComponent<TrackableObject>();
            if (trackableObject != null && !feedbacks.Exists(feedback => feedback.GetTrackableObject().GetInstanceID() == trackableObject.GetInstanceID())) {
                feedbacks.Add(new Feedback(trackableObject));
            }
        }
        */
        float prosentage = feedbacks.Sum(feedback => feedback.GetThreshold());
        CheckIfListIsValid("Feedbacks", feedbacks.Any());
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

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
