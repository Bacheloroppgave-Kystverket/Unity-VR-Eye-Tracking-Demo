using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Utilities for opening and closing the hand menu.
/// </summary>
public class HandUIUtilities : MonoBehaviour
{
    [SerializeField]
    private GameObject leftHandIconPosition, rightHandIconPosition;

    [SerializeField]
    private GameObject menu;

    [SerializeField]
    private Vector3 openPosition;

    [SerializeField]
    private Vector3 closedPosition;

    [SerializeField]
    private float toggleTime;
    private bool isOpen;
    private Vector3 initialScale;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = menu.activeSelf;
        openPosition = menu.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMenu() {
        if(isOpen) {
            StartCoroutine(DisableAndCloseMenu());
        } else {
            StartCoroutine(EnableAndOpenMenu());
        }
    }

    public void EnableMenu() {
        Debug.Log("Opening hand menu.");
       // menu.transform.position = openPosition;
        isOpen = true;
        menu.SetActive(isOpen);
    }

    public void DisableMenu() {
        Debug.Log("Closing hand menu.");
        isOpen = false;
        menu.SetActive(isOpen);
    }

    private IEnumerator DisableAndCloseMenu() {
        StartCoroutine(LerpToPosition(openPosition, leftHandIconPosition.transform.position, toggleTime));
        yield return new WaitForSeconds(toggleTime);
        DisableMenu();
    }

    private IEnumerator EnableAndOpenMenu() {
        EnableMenu();
        StartCoroutine(LerpToPosition(leftHandIconPosition.transform.position,openPosition, toggleTime));
        yield return null;
    }
    
    IEnumerator LerpVector3OverTime(Vector3 initialVector, Vector3 targetRotation, float seconds) {
        float timeElapsed = 0;
        while (initialVector != targetRotation && timeElapsed < 1) {
            menu.transform.position = Vector3.Lerp(initialVector, targetRotation, timeElapsed / seconds);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator LerpToPosition(Vector3 fromPosition, Vector3 toPosition, float seconds) {
        float timeElapsed = 0;
        //openPosition = menu.transform.position;
        while (menu.transform.position != toPosition && timeElapsed <1) {
            menu.transform.position = Vector3.Lerp(fromPosition, toPosition, timeElapsed/seconds);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
