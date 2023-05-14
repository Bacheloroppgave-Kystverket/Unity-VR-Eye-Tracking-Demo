using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Launches an object to one side and switches the launch direction after a while.
/// </summary>
public class LaunchObject : MonoBehaviour
{

    [SerializeField]
    private Rigidbody rigidbody;

    [SerializeField]
    private int force;

    private bool forward = true;

    private bool firstTime = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SwitchDirection());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Switches the direction of the launch.
    /// </summary>
    /// <returns>the enumerator</returns>
    private IEnumerator SwitchDirection() {
        yield return new WaitForSeconds(3);
        while (true) {
            if (forward){
                float times = firstTime ? 1 : 2;
                rigidbody.AddForce(Vector3.forward * (force * times));
                forward = false;
                firstTime = false;
            }
            else
            {
                forward = true;
                rigidbody.AddForce(Vector3.back * force * 2);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
