using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IncrementController : MonoBehaviour
{
    [SerializeField, Tooltip("The plus buttons")]
    private List<Button> plusButtons = new List<Button>();

    [SerializeField, Tooltip("The minus buttons")]
    private List<Button> minusButtons = new List<Button>();

    [SerializeField, Tooltip("The amount of the increment")]
    private TextMeshProUGUI amountText;

    [SerializeField, Tooltip("The current value")]
    private int currentValue = 0;

    [SerializeField, Tooltip("The max value for this increment controller")]
    private float maxValue;

    [SerializeField, Tooltip("The other increment controller")]
    private IncrementController otherIncrementController;

    [SerializeField, Tooltip("True if this increment controller is the max value. False otherwise")]
    private bool isMaxValue;

    private void Start()
    {
        SetCurrentValue(currentValue);
    }

    /// <summary>
    /// Incremenets the value of the controller.
    /// </summary>
    /// <param name="value">the value to add</param>
    public void IncremenetValue(int value) {
        int newValue = this.currentValue + value;
        bool validValue = CheckValueAccordingToOther(newValue);
        if (newValue < 1 && validValue)
        {
            newValue = 1;
            minusButtons.ForEach(button => button.enabled = false);
            plusButtons.ForEach(button => button.enabled = true);
            SetCurrentValue(newValue);
        }
        else if (newValue > maxValue && validValue)
        {
            newValue = Mathf.FloorToInt(maxValue);
            plusButtons.ForEach(button => button.enabled = false);
            minusButtons.ForEach(button => button.enabled = true);
            SetCurrentValue(newValue);
        }
        else if(validValue) {
            SetCurrentValue(newValue);
        }
    }

    /// <summary>
    /// Sets the current value and updates the text.
    /// </summary>
    /// <param name="value">the value</param>
    private void SetCurrentValue(int value) {
        this.currentValue = value;
        amountText.text = value.ToString();
    }

    /// <summary>
    /// Checks if the value is lower or higher than the other one.
    /// </summary>
    /// <param name="newValue">the new value</param>
    /// <returns>True if the value is valid according to the other. False otherwise</returns>
    private bool CheckValueAccordingToOther(int newValue) { 
        return isMaxValue ? newValue > otherIncrementController.GetCurrentValue() : newValue < otherIncrementController.GetCurrentValue();
    }

    /// <summary>
    /// Gets the current value.
    /// </summary>
    /// <returns>the current value</returns>
    public int GetCurrentValue() => currentValue;

    /// <summary>
    /// Sets the max value.
    /// </summary>
    /// <param name="maxValue">the max value</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the max value is lower than zero</exception>
    public void SetMaxValue(float maxValue) {
        if (maxValue < 0) {
            throw new IllegalArgumentException("The max value cannot be lower than zero");
        }
        this.maxValue = maxValue;
        SetCurrentValue(isMaxValue ? 1 : 0);
    }
}
