using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Changes the gameobject's rotation to face the player.
/// </summary>
public class LookAtPlayer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Whether or not the object should look at the player on this axis.")]
    private bool rotateX, rotateY, rotateZ = true;
    [SerializeField]
    [Tooltip("Whether the object should face away from the player or not")]
    private bool inverted = false;

    private Transform target;

    // Start is called before the first frame update
    void Start(){
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    /// <inheritdoc/>
    private void FixedUpdate()
    {
        Vector3 initialRotation = gameObject.transform.rotation.eulerAngles;
          gameObject.transform.LookAt(target);

        //Ensures that assigned axes are ignored. 
        if (!rotateX) {
            gameObject.transform.rotation = Quaternion.Euler(initialRotation.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
            

        if(!rotateY) {
            gameObject.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, initialRotation.y, transform.rotation.eulerAngles.z);

        }

        if (!rotateZ) {
            gameObject.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, initialRotation.z);

        }

        if(inverted) {
            gameObject.transform.rotation *= Quaternion.Euler(transform.rotation.eulerAngles * -1);
        }
    }
}
