using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This manager has a responsebility to handle the feedback that the user gets through the adaptive training system.
/// </summary>
[SerializeField]
public class FeedbackManager : MonoBehaviour{

    [SerializeField, Tooltip("The trackable objects that is sorted in a map")]
    private SortedTrackableObjectsMap sortedTrackableObjectsMap;

    [Space(10), Header("Managers")]
    [SerializeField, Tooltip("The session manager")]
    private SessionManager sessionManager;

    [SerializeField, Tooltip("The reference position manager")]
    private ReferencePositionManager referencePositionManager;

    [SerializeField, Tooltip("The overlay manager")]
    private OverlayManager overlayManager;

    [SerializeField, Tooltip("Set to true in order to visualize keys and values")]
    private bool visualizeKeysAndValues = true;


    // Start is called before the first frame update
    void Start(){
        CheckField("Session manager", sessionManager);
        CheckField("Reference position manager", referencePositionManager);
        sortedTrackableObjectsMap = new SortedTrackableObjectsMap(sessionManager.GetSession().GetCloseTrackableObjects(), visualizeKeysAndValues);
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
    /// Calculates and displays the feedback to the user.
    /// </summary>
    public void CalculateAndDisplayProsentageFeedback() {
        SessionController session = sessionManager.GetSession();
        ProsentageTypeFeedback feedback = new ProsentageTypeFeedback(CalculateProsentageWatchedForSeat());
        session.AddFeedback(feedback);
        overlayManager.DisplayFeedback(feedback);
    }

    /// <summary>
    /// Calculates the prosentage watched for the current seat.
    /// </summary>
    /// <returns>the calculated result per trackable type</returns>
    public List<CalculatedFeedback> CalculateProsentageWatchedForSeat()
    {
        List<CalculatedFeedback> prosentageList = new List<CalculatedFeedback>();

        ReferencePositionController referencePosition = referencePositionManager.GetCurrentReferencePosition();
        float timeForPosition = referencePosition.GetPositionDuration();
        IEnumerator<TrackableType> it = sortedTrackableObjectsMap.GetEnumerator();
        float totalTime = 0;
        float prosentage = 0;
        while (it.MoveNext())
        {
            float totalTypeTime = 0;
            int i = 0;
            TrackableType trackableType = it.Current;
            List<TrackableObjectController> trackableObjects = sortedTrackableObjectsMap.GetListForTrackableType(trackableType);
            foreach (TrackableObjectController trackableObject in trackableObjects)
            {
                totalTypeTime += trackableObject.GetGazeDataForPosition(referencePosition.GetLocationId()).GetFixationDuration();
            }
            totalTime += totalTypeTime;
            prosentageList.Add(new CalculatedFeedback(trackableType, FindProsentage(totalTypeTime, timeForPosition)));
            prosentage += prosentageList.Last().GetProsentage();
            i++;
        }
        prosentageList.Add(new CalculatedFeedback(TrackableType.OTHER, 100 - FindProsentage(totalTime, timeForPosition)));
        prosentage += prosentageList.Last().GetProsentage();
        prosentageList.Add(new CalculatedFeedback(TrackableType.UNDEFINED, 100 - prosentage));

        return prosentageList;
    }

    /// <summary>
    /// Finds the prosentage of the time.
    /// </summary>
    /// <param name="time">the time</param>
    /// <param name="timeForPosition">the time for the position</param>
    /// <returns></returns>
    private float FindProsentage(float time, float timeForPosition)
    {
        return Mathf.Round(((time / timeForPosition)) * 100);
    }
}
