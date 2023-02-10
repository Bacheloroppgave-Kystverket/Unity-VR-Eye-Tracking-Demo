using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackingManager : MonoBehaviour
{

    [SerializeField]
    private List<TrackableObject> trackableObjects;

    [SerializeField]
    private float time;

    [SerializeField]
    private SessionManager sessionManager;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        CheckField("Session manager", sessionManager);
        CheckIfListIsValid("Trackable objects", trackableObjects.Any());
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    /// <summary>
    /// Checks if the list has any elements.
    /// </summary>
    /// <param name="error">the error to display</param>
    /// <param name="hasAny">true if the list has any. False otherwise</param>
    private void CheckIfListIsValid(string error, bool hasAny) {
        if (!hasAny) {
            Debug.Log("<color=red>Error:</color>" + error + " cannot be emtpy.", gameObject);
        }
    }

    /// <summary>
    /// Checks if the defined field is set in the editor.
    /// </summary>
    /// <param name="error">the type of error like "type of object"</param>
    /// <param name="fieldToCheck">The field to check</param>
    private bool CheckField(string error, object fieldToCheck) {
        bool valid = fieldToCheck == null;
        if (valid) {
            Debug.Log("<color=red>Error:</color>" + error + " must be set.", gameObject);
        }
        return valid;
    }

    /// <summary>
    /// Calculates the average fixation time per object.
    /// </summary>
    public void CalculateAverageFixationTimePerObject() {
        trackableObjects.ForEach(trackableObject => trackableObject.CalculateCurrentAverageFixationDuration(sessionManager.GetCurrentReferencePosition().GetLocationId()));
    }
}
