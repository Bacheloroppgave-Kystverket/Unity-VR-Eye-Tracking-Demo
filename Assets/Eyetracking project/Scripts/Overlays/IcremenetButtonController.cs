using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IcremenetButtonController : MonoBehaviour
{
    [SerializeField, Tooltip("The value that the button represents")]
    private int value = 0;

    [SerializeField, Tooltip("The text of the button")]
    private TextMeshProUGUI buttonText;

    [SerializeField, Tooltip("The increment controller")]
    private IncrementController incrementController;

    private void Start()
    {
        buttonText.text = value.ToString();
    }

    /// <summary>
    /// Hits the button and increments or decrements the value.
    /// </summary>
    public void ButtonHit() {
        incrementController.IncremenetValue(value);
    }


}
