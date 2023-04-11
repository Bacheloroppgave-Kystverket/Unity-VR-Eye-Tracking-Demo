using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes a gameobject look at the player.
/// </summary>
public class LookAtPlayer : MonoBehaviour
{
    private Transform target;

    // Start is called before the first frame update
    void Start(){
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    /// <inheritdoc/>
    private void FixedUpdate()
    {
        gameObject.transform.LookAt(target);
    }
}
