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
                TextMeshPro text = Instantiate(prefab, this.transform);
                
                text.transform.position = middle;
                //textMap.Add(oldTransform.gameObject.GetInstanceID(), text);
                oldTransform = newTransform;
                oldPos = end;
            }
        }
        
        
        
        
        
    }

}
