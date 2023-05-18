using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Utilities for opening and closing the hand menu.
/// </summary>
public class HandUIUtilities : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The transform of the icon the menu will go to")]
    private GameObject handIconPosition;

    [SerializeField]
    [Tooltip("The menu that the utilities will act upon")]
    private GameObject menu;

    [SerializeField]
    [Tooltip("The position the menu will have when it is open")]
    private Vector3 openPosition;

    [SerializeField]
    [Tooltip("The position the menu will have when it is closed")]
    private Vector3 closedPosition;

    [SerializeField]
    [Tooltip("The time it takes for the menu to switch between the open and closed state in seconds")]
    private float toggleTime;
    
    private bool isOpen;
    private Vector3 initialScale;
    private Vector3 initialRotation;

    /// <summary>
    /// Activates itself, and initializes which positions are relevant for open and closed. 
    /// </summary>
    void Start()
    {
        isOpen = menu.activeSelf;
        openPosition = menu.transform.position;
        initialScale = menu.transform.localScale;
        initialRotation = menu.transform.rotation.eulerAngles;
    }

    /// <summary>
    /// Opens the menu if it is closed, closes the menu if it is open. 
    /// </summary>
    public void ToggleMenu() {
        if(isOpen) {
            StartCoroutine(DisableAndCloseMenu());
        } else {
            StartCoroutine(EnableAndOpenMenu());
        }
    }

    /// <summary>
    /// Enables the menu gameobject. 
    /// </summary>
    public void EnableMenu() {
        Debug.Log("Opening hand menu.");
       // menu.transform.position = openPosition;
        isOpen = true;
        menu.SetActive(isOpen);
    }

    /// <summary>
    /// Disables the menu gameobject.
    /// </summary>
    public void DisableMenu() {
        Debug.Log("Closing hand menu.");
        isOpen = false;
        menu.SetActive(isOpen);
    }

    /// <summary>
    /// Disables the menu and moves it to the closed position
    /// </summary>
    /// <returns></returns>
    private IEnumerator DisableAndCloseMenu() {
        openPosition = menu.transform.position;
        StartCoroutine(LerpToPosition(openPosition, handIconPosition.transform.position, toggleTime));
        StartCoroutine(LerpToScale(initialScale, Vector3.zero, toggleTime));
        StartCoroutine(LerpToRotation(initialRotation, handIconPosition.transform.rotation.eulerAngles, toggleTime));
        yield return new WaitForSeconds(toggleTime);
        DisableMenu();
    }

    /// <summary>
    /// Enables the menu and moves it to the open position
    /// </summary>
    /// <returns></returns>
    private IEnumerator EnableAndOpenMenu() {
        EnableMenu();
        StartCoroutine(LerpToPosition(handIconPosition.transform.position,openPosition, toggleTime));
        StartCoroutine(LerpToScale(Vector3.zero, initialScale, toggleTime));
        StartCoroutine(LerpToRotation(handIconPosition.transform.rotation.eulerAngles, /**initialRotation*/Vector3.zero, toggleTime));
        yield return null;
    }
    
    /// <summary>
    /// Lerps from a rotation to another rotation over a given amount of seconds.
    /// </summary>
    /// <param name="fromRotation">The initial rotation</param>
    /// <param name="toRotation">The rotation that will be lerped into</param>
    /// <param name="seconds">The amount of time the rotation will take</param>
    /// <returns></returns>
    IEnumerator LerpToRotation(Vector3 fromRotation, Vector3 toRotation, float seconds) {
        float timeElapsed = 0;
        while (menu.transform.rotation.eulerAngles != toRotation && timeElapsed < 1) {
           // toRotation = leftHandIconPosition.transform.rotation.eulerAngles;
            menu.transform.SetPositionAndRotation(menu.transform.position, Quaternion.Euler(Vector3.Lerp(fromRotation, toRotation, timeElapsed / seconds)));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// Lerps from a scale to another scale over a given amount of seconds.
    /// </summary>
    /// <param name="fromScale">The initial scale</param>
    /// <param name="toScale">The scale that will be lerped into</param>
    /// <param name="seconds">The amount of time the scaling will take</param>
    /// <returns></returns>
    IEnumerator LerpToScale(Vector3 fromScale, Vector3 toScale, float seconds) {
        float timeElapsed = 0;
        while (menu.transform.localScale != toScale && timeElapsed < 1) {
            menu.transform.localScale = Vector3.Lerp(fromScale, toScale, timeElapsed / seconds);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// Lerps from a given position to a new position over a set amount of time. 
    /// </summary>
    /// <param name="fromPosition">The initial position</param>
    /// <param name="toPosition">The position that will be lerped into</param>
    /// <param name="seconds">The amount of time the positioning will take</param>
    /// <returns></returns>
    IEnumerator LerpToPosition(Vector3 fromPosition, Vector3 toPosition, float seconds) {
        float timeElapsed = 0;
        while (menu.transform.position != toPosition && timeElapsed <1) {
            menu.transform.position = Vector3.Lerp(fromPosition, toPosition, timeElapsed/seconds);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
