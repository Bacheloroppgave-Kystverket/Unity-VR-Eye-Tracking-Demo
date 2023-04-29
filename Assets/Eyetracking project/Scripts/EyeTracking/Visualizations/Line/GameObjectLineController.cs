using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameObjectLineController : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField, Tooltip("The cube within the empty")]
    private Transform lineCube;

    [SerializeField, Tooltip("The text of the line.")]
    private TextMeshPro textOfLine;

    [Header("Other")]
    [SerializeField, Tooltip("The transform that we are going from.")]
    private Transform fromTransform;

    [SerializeField, Tooltip("The tranform that we are going to.")]
    private Transform toTransform;

    [SerializeField, Tooltip("The display text")]
    private string displayText;

    private bool zeroDistance;

    /// <summary>
    /// Sets the new positions. 
    /// </summary>
    /// <param name="fromTransform">the from transform</param>
    /// <param name="toTransform">the to transform.</param>
    public void SetNewPositonsAndUpdateLine(Transform fromTransform, Transform toTransform) {
        SetFromTransform(fromTransform);
        SetToTransform(toTransform);
        UpdateLinePosition();
    }
    
    /// <summary>
    /// Updates the line position.
    /// </summary>
    private void UpdateLinePosition() {
        ShowLine();
        Vector3 newPos = (fromTransform.position + toTransform.position)/2;
        transform.position = newPos;
        lineCube.LookAt(toTransform);
        float distance = Vector3.Distance(toTransform.position, fromTransform.position);
        zeroDistance = distance < 0;
        if (!zeroDistance)
        {
            Vector3 scale = lineCube.transform.localScale;
            lineCube.transform.localScale = new Vector3(scale.x, scale.y, distance - 0.015f);
        }
        else {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Sets a new from transform.
    /// </summary>
    /// <param name="transform">the from transform</param>
    private void SetFromTransform(Transform transform) {
        CheckIfObjectIsNull(transform, "transform");
        this.fromTransform = transform;
    }

    /// <summary>
    /// Hides the line from the world.
    /// </summary>
    public void HideLine() { 
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Shows the line.
    /// </summary>
    public void ShowLine() {
        gameObject.SetActive(!zeroDistance);
    }

    /// <summary>
    /// Sets a new to transform.
    /// </summary>
    /// <param name="transform">the new to transform</param>
    public void SetToTransform(Transform transform) {
        CheckIfObjectIsNull(transform, "transform");
        this.toTransform  = transform;
    }

    /// <summary>
    /// Checks if the object is null or not. Throws an exception if the object is null.
    /// </summary>
    /// <param name="objecToCheck">the object to check</param>
    /// <param name="error">the error to be in the string.</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the object to check is null.</exception>
    private void CheckIfObjectIsNull(object objecToCheck, string error)
    {
        if (objecToCheck == null)
        {
            throw new IllegalArgumentException("The " + error + " cannot be null.");
        }
    }
}
