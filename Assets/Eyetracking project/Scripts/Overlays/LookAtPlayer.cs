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
        //grabableObject.transform.LookAt(target);
        Vector3 lookRotation = Quaternion.LookRotation(target.position - transform.position).eulerAngles;

        //Ensures that assigned axes are ignored. 
        if (!rotateX) {
            //  grabableObject.transform.rotation = Quaternion.Euler(initialRotation.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            lookRotation = new Vector3(initialRotation.x, lookRotation.y, lookRotation.z);

        }

        if (!rotateY) {
            //grabableObject.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, initialRotation.y, transform.rotation.eulerAngles.z);
            lookRotation = new Vector3(lookRotation.x, initialRotation.y, lookRotation.z);
        }

        if (!rotateZ) {
            //grabableObject.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, initialRotation.z);
            lookRotation = new Vector3 (lookRotation.x, lookRotation.y, initialRotation.z);
        }

        if(inverted) {
            //grabableObject.transform.rotation *= Quaternion.Euler(transform.rotation.eulerAngles * -1);
            lookRotation *= -1;
        }
        transform.eulerAngles = lookRotation;
    }
}
