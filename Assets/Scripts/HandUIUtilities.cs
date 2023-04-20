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
    // Start is called before the first frame update
    void Start()
    {
        isOpen = menu.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMenu() {
        if(isOpen) {
            StartCoroutine(DisableAndCloseMenu());
        } else {
            EnableMenu();
        }
    }

    public void EnableMenu() {
        Debug.Log("Opening hand menu.");
        menu.transform.position = openPosition;
        isOpen = true;
        menu.SetActive(isOpen);
    }

    public void DisableMenu() {
        Debug.Log("Closing hand menu.");
        isOpen = false;
        menu.SetActive(isOpen);
    }

    IEnumerator DisableAndCloseMenu() {
        StartCoroutine(LerpToPosition(leftHandIconPosition.transform.position, toggleTime));
        yield return new WaitForSeconds(toggleTime);
        DisableMenu();
    }

    IEnumerator LerpToPosition(Vector3 newPosition, float seconds) {
        float timeElapsed = 0;
        openPosition = menu.transform.position;
        while (menu.transform.position != newPosition && timeElapsed <1) {
            menu.transform.position = Vector3.Lerp(openPosition, newPosition, timeElapsed/seconds);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
