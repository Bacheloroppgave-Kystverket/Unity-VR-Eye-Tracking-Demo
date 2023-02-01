using UnityEngine;
using UnityEngine.UI;

public class RayCasterObject : MonoBehaviour
{
    [SerializeField]
    private bool casting = false;

    public GameObject gameObject;

    public TrackableObject lastObject;

    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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


    private void FixedUpdate()
    {
        if (casting){
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit raycastHit;
            // Physics.Raycast(ray, out raycastHit);
            Physics.SphereCast(transform.position, 0.03f, transform.forward, out raycastHit);
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
                    trackObject.ToggleIsWatched();
                    lastObject = trackObject;
                }
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycastHit.distance);
            }
            else {
                if (lastObject != null) {
                    lastObject.ToggleIsWatched();
                    lastObject = null;
                }
            }
        }
        else {
            gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
            if (lastObject != null) {
                lastObject.ToggleIsWatched();
                lastObject = null;
            }
        }
    }
}
