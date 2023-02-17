using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReferencePositionManager : MonoBehaviour
{
    [Header("Reference position")]
    [SerializeField, Tooltip("This list will populate itself with all the objects once the session starts")]
    private List<ReferencePosition> referencePositions = new List<ReferencePosition>();

    [SerializeField]
    private int position = 0;

    [Header("Player object")]
    [SerializeField]
    private GameObject playerObject;

    [SerializeField, Tooltip("The trackable objects manager.")]
    private TrackableObjectsManager trackableObjectsManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] referenceObjects = GameObject.FindGameObjectsWithTag(typeof(ReferencePosition).Name);
        foreach (GameObject rfObject in referenceObjects.Reverse())
        {
            ReferencePosition referencePosition = rfObject.GetComponent<ReferencePosition>();
            referencePositions.Add(referencePosition);
        }
        CheckField("Player", playerObject);
        CheckIfListIsValid("Reference positions", referencePositions.Any());
        CheckField("Trackable Object Manager", trackableObjectsManager);
        trackableObjectsManager.UpdatePositionOnAllTrackableObjects(referencePositions[position].GetLocationId());
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
    private void CheckIfListIsValid(string error, bool hasAny)
    {
        if (!hasAny)
        {
            Debug.Log("<color=red>Error:</color>" + error + " cannot be emtpy.", gameObject);
        }
    }

    /// <summary>
    /// Gets the currernt reference position.
    /// </summary>
    /// <returns>the current reference position</returns>
    public ReferencePosition GetCurrentReferencePosition() {
        return referencePositions[position];
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentReferencePosition().AddTime();
    }

    /// <summary>
    /// Goes to the next reference position.
    /// </summary>
    public void NextPosition()
    {
        position = (position + 1) % referencePositions.Count;
        MonoBehaviour.print("Position: " + position);
        playerObject.gameObject.transform.position = GetCurrentReferencePosition().gameObject.transform.position;
        trackableObjectsManager.UpdatePositionOnAllTrackableObjects(GetCurrentReferencePosition().GetLocationId());
    }
}
