using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BoatController : MonoBehaviour
{
    [SerializeField, Tooltip("The agent")]
    private NavMeshAgent agent;

    [SerializeField, Tooltip("The targets to navigate to.")]
    private List<Transform> targets = new List<Transform>();

    [SerializeField, Tooltip("The target position.")]
    private Vector3 targetPostion;

    [SerializeField, Tooltip("The position")]
    private int pos = 0;

    [SerializeField, Tooltip("True if the boat is supposed to move.")]
    private bool startMoving;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pos = -1;
        targetPostion = transform.position;
        transform.localRotation = Quaternion.Euler(0, 90, 0);
        StartCoroutine(StartMotion());
    }


    public IEnumerator StartMotion() {
        while (startMoving) {
            if (Vector3.Distance(transform.position, targetPostion) < 4f)
            {
                pos = (pos + 1) % targets.Count ;
                Vector3 newVector = targets[pos].position;
               
                targetPostion = newVector;
            }
            if (agent.velocity == Vector3.zero) {
                agent.SetDestination(targetPostion);
            }
            yield return new FixedUpdate();
        }
    }

}
