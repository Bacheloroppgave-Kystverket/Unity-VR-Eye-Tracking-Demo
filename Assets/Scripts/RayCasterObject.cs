using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a object that can cast rays. 
/// </summary>
public abstract class RayCasterObject : MonoBehaviour
{
    [SerializeField]
    private bool casting = false;

    [SerializeField]
    private TrackableObject lastObject;

    [SerializeField, Min(0.01f)]
    private float sphereSize = 0.03f;

    [SerializeField]
    private Session session;

    [SerializeField]
    private GameObject hitSpot;


    public void ToggleIsCasting() {
        casting = !casting;
    }

    /// <summary>
    /// Sets if the raygun shoud cast.
    /// </summary>
    /// <param name="isCasting">true if it should cast</param>
    public void SetIsCasting(bool isCasting) {
        this.casting = isCasting;
    }

    public bool IsCasting()
    {
        return casting;
    }

    /// <summary>
    /// Gets the position to shoot the ray from.
    /// </summary>
    /// <returns>the position</returns>
    protected abstract Vector3 FindPosition();

    protected abstract Vector3 FindDirection();

    private void FixedUpdate()
    {
        if (casting){
            //Makes ray
            RaycastHit raycastHit;
            //Shoots ray
            Vector3 postion = FindPosition();
            //Physics.SphereCast(postion, sphereSize, FindDirection(), out raycastHit);
            Physics.Raycast(postion, FindDirection(), out raycastHit);
            if (raycastHit.collider != null)
            {
                TrackableObject trackObject = raycastHit.collider.gameObject.GetComponent<TrackableObject>();
                if (lastObject == null || !GameObject.ReferenceEquals(trackObject, lastObject))
                {
                    if (lastObject != null && lastObject.IsWatched())
                    {
                        lastObject.ToggleIsWatched();
                        lastObject = null;
                    }
                    trackObject.ToggleIsWatched(session.GetCurrentReferencePosition().GetLocationId());
                    lastObject = trackObject;
                }
                hitSpot.transform.position = raycastHit.point;
                Debug.DrawRay(postion, FindDirection() * raycastHit.distance);
            }
            else {
                if (lastObject != null) {
                    lastObject.ToggleIsWatched();
                    lastObject = null;
                }
            }
        }
        else {
            if (lastObject != null) {
                lastObject.ToggleIsWatched();
                lastObject = null;
            }
        }
    }
}
