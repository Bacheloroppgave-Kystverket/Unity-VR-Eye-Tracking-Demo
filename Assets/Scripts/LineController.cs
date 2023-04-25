using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Polybrush;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField, Tooltip("Game objects to draw a line between.")]
    private List<Transform> transforms = new List<Transform>();

    [SerializeField, Tooltip("The textmesh prefab")]
    private TextMeshPro prefab;

    [SerializeField, Tooltip("The connection between the dots prefab.")]
    private GameObjectLineController linePrefab;

    [SerializeField, Tooltip("The gameobject line controllers")]
    private List<GameObjectLineController> gameObjectLineControllers = new List<GameObjectLineController>();

    [SerializeField, Tooltip("Set to true if the line text is supposed to be displayed. False otherwise.")]
    private bool displayLineText;

    public void ClearLineList() {
        transforms.Clear();
        HideLine();
    }

    /// <summary>
    /// Hides the lines.
    /// </summary>
    public void HideLine()
    {
        gameObjectLineControllers.ForEach(lineController => lineController.HideLine());
    }

    /// <summary>
    /// Adds a transform to the line controller.
    /// </summary>
    /// <param name="transform">the transform to add</param>
    public void AddTransform(Transform transform) {
        CheckIfObjectIsNull(transform, "transform");
        transforms.Add(transform);
    }

    /// <summary>
    /// Draws the line from the first transform to the last.
    /// </summary>
    public void DrawLine() {
        int index = 0;
        IEnumerator<Transform> it = transforms.GetEnumerator();
        Transform oldTrans = null;
        while (it.MoveNext()) {
            Transform currentTrans = it.Current;
            if (oldTrans != null) {
                SetLine(index, oldTrans, currentTrans);
                index++;
            }
            oldTrans = currentTrans;
        }
    }

    /// <summary>
    /// Sets the linecontroller at that position. If there is none the linecontroller is added.
    /// </summary>
    /// <param name="index">the index</param>
    /// <param name="fromTransform">the from transform</param>
    /// <param name="toTransform">the to transform</param>
    private void SetLine(int index, Transform fromTransform, Transform toTransform)
    {
        GameObjectLineController lineController;
        if (index == gameObjectLineControllers.Count)
        {
            lineController = Instantiate(linePrefab, transform);
            gameObjectLineControllers.Add(lineController);
        }
        else {
            lineController = gameObjectLineControllers[index];
        }
        lineController.SetNewPositonsAndUpdateLine(fromTransform, toTransform, displayLineText ? (index + 1).ToString() : "");
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
