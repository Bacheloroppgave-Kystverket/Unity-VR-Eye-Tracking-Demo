using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// This manager has a responsebility to handle the feedback that the user gets through the adaptive training system.
/// </summary>
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

    [SerializeField, Tooltip("The interval that the adaptive training system should be active for."), Min(10)]
    private int seconds = 10;

    [SerializeField, Tooltip("The amount of remaining seconds")]
    private float remainingSeconds = 0;

    [SerializeField, Tooltip("Set to true if the adaptive training system should be running.")]
    private bool activeAdaptiveTrainingSystem = true;

    [SerializeField, Tooltip("Set to true if eyetracking is done.")]
    private bool eyeTracking = false;

    private bool hasMadeMap;


    // Start is called before the first frame update
    void Start(){
        CheckField("Session manager", sessionManager);
        CheckField("Reference position manager", referencePositionManager);

    }


    public void StartEyetracking() {
        if (!hasMadeMap) {
            sortedTrackableObjectsMap = new SortedTrackableObjectsMap(sessionManager.GetSessionController().GetSimulationSetupController().GetCloseTrackableObjects(), visualizeKeysAndValues);
            hasMadeMap = true;
        }
        if (!eyeTracking) {
            eyeTracking = true;
            StartCoroutine(StartAdaptiveTrainingSystem());
        }
    }

    public void StopEyeTracking() {
        eyeTracking = false;
    }


    /// <summary>
    /// Starts the adaptive training system.
    /// </summary>
    /// <returns>the wait time</returns>
    private IEnumerator StartAdaptiveTrainingSystem() {
        yield return new WaitForSeconds(0.25f);
        while (activeAdaptiveTrainingSystem && eyeTracking) {
            this.remainingSeconds += 0.25f;
            if (remainingSeconds >= seconds) {
                CalculateAndDisplayProsentageFeedback();
                remainingSeconds = 0;
            }
            yield return new WaitForSeconds(0.25f);
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

    /// <summary>
    /// Calculates and displays the feedback to the user.
    /// </summary>
    private void CalculateAndDisplayProsentageFeedback() {
        SessionController session = sessionManager.GetSessionController();
        IEnumerator<ReferencePositionController> it = referencePositionManager.GetEnumeratorForReferencePositions();
        ReferencePosition currentPosition = referencePositionManager.GetCurrentReferencePosition().GetReferencePosition();
        while (it.MoveNext()) {
            ReferencePositionController position = it.Current;
            AdaptiveFeedback adaptiveFeedback = CalculateAdaptiveFeedbackForPosition(position);
            session.GetPositionRecord(position.GetReferencePosition()).AddFeedback(adaptiveFeedback);
            if (currentPosition == position.GetReferencePosition()) {
                overlayManager.DisplayFeedback(adaptiveFeedback);
                //overlayManager.DisplayLeastViewedObject(adaptiveFeedback, position.GetReferencePosition());
            }
        }
    }

    /// <summary>
    /// Calculates the adaptive feedback for the position.
    /// </summary>
    /// <param name="referencePositionController">the reference position</param>
    /// <returns>the adaptive feedback</returns>
    private AdaptiveFeedback CalculateAdaptiveFeedbackForPosition(ReferencePositionController referencePositionController) {
        List<CategoryFeedback> categoryFeedbacks = new List<CategoryFeedback>();
        IEnumerator<TrackableType> it = sortedTrackableObjectsMap.GetEnumerator();
        while (it.MoveNext()) {
            TrackableType trackableType = it.Current;
            float totalTime = 0;
            foreach (TrackableObjectController trackableController in sortedTrackableObjectsMap.GetListForTrackableType(trackableType)) {
                totalTime += trackableController.GetGazeDataForPosition(referencePositionController.GetReferencePosition()).GetFixationDuration();
            }
            categoryFeedbacks.Add(new CategoryFeedback(trackableType, totalTime));
        }
        return new AdaptiveFeedback(referencePositionController.GetPositionDuration(), categoryFeedbacks) ;
    }
}
