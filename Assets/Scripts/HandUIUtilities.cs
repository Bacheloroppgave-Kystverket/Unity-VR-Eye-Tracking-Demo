using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Utilities for opening and closing the hand menu.
/// </summary>
public class HandUIUtilities : MonoBehaviour
{

    private GameObject leftHand;
    private GameObject rightHand;

    [SerializeField]
    private GameObject menu;
    private Vector3 openPosition;
    private Vector3 closedPosition;
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
            CloseMenu();
        } else {
            OpenMenu();
        }
    }

    public void OpenMenu() {
        Debug.Log("Opening hand menu.");
        isOpen = true;
        menu.SetActive(isOpen);
    }

    public void CloseMenu() {
        Debug.Log("Closing hand menu.");
        isOpen = false;
        menu.SetActive(isOpen);
    }

    private void LerpToPosition() {
        
    }
}
