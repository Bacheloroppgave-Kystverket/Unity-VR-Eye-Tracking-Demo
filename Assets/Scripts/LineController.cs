using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Polybrush;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(LineRenderer))]
public class LineController : MonoBehaviour
{
    [SerializeField, Tooltip("Game objects to draw a line between.")]
    private List<Transform> transforms = new List<Transform>();

    private LineRenderer lineRenderer;

    [SerializeField, Tooltip("The textmesh prefab")]
    private TextMeshPro prefab;

    private void Start()
    {
        //textMap = new Dictionary<int, TextMeshPro>();
        this.lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetTextForGameObject() { 
    
    }

    public void ClearLineList() {
        transforms.Clear();
        HideLine();
    }

    public void HideLine() {
        lineRenderer.enabled = false;
    }

    public void SetTransforms(List<Transform> transforms) {
        CheckIfObjectIsNull(transforms, "transforms");
        this.transforms = transforms;
    }

    public void UpdateLines() {
        lineRenderer.SetPositions(new List<Vector3>().ToArray());
        DrawLine();
    }

    public void DrawLine() {
        
        if (transforms.Count > 2) {
            IEnumerator<Transform> it = transforms.GetEnumerator();
            it.MoveNext();
            Transform oldTransform = it.Current;
            int i = 0;
            Vector3 oldPos = oldTransform.position;
            lineRenderer.positionCount = transforms.Count;
            lineRenderer.SetPosition(i, oldPos);
            i++;

            
            while (it.MoveNext())
            {
                Transform newTransform = it.Current;
                Vector3 end = newTransform.transform.position;
                Vector3 middle = (oldPos + end)/2;
                
                lineRenderer.SetPosition(i, end);
                i++;
                /*
                TextMeshPro text = Instantiate(prefab, this.transform);
                
                text.transform.position = middle;
                */
                //textMap.Add(oldTransform.gameObject.GetInstanceID(), text);
                oldTransform = newTransform;
                oldPos = end;
            }
            lineRenderer.enabled = true;
        }
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
