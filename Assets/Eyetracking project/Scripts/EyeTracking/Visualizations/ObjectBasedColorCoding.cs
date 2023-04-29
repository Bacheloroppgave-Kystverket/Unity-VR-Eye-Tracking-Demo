using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrackableObjectController))]
public class ObjectBasedColorCoding : MonoBehaviour
{
    [SerializeField, Tooltip("The prefab for the box")]
    private GameObject prefab;

    [SerializeField, Tooltip("The colored part of the object")]
    private GameObject colorObject;


    public void SetValues(Metric metric, float total) {
        if (colorObject == null) {
            MakeColorObject();
        }
        GazeData gazeData = GetComponent<TrackableObjectController>().GetCurrentGazeData();
        float value = metric == Metric.FIXATIONS ? gazeData.GetFixations() : gazeData.GetFixationDuration();
        float prosentage = value / total;
        if (prosentage > 0.5f)
        {
            colorObject.GetComponent<Material>().color = Color.green;
        }
        else if (prosentage > 0.25f)
        {
            colorObject.GetComponent<Material>().color = Color.blue;
        }
        else {
            colorObject.GetComponent<Material>().color = Color.red;
        }
    }

    public void MakeColorObject() {
        this.colorObject = Instantiate(prefab);
        this.colorObject.transform.SetParent(this.transform);
        this.colorObject.transform.localScale.Set(transform.localScale.x + 0.1f, transform.localScale.y + 0.1f, transform.localScale.z + 0.1f);
    }

    /// <summary>
    /// Hides the object 
    /// </summary>
    public void HideObject() {
        if (colorObject != null) {
            colorObject.SetActive(false);
        }
    }
}
