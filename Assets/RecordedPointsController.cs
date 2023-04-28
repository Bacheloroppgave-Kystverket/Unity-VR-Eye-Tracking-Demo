using Oculus.Installer.ThirdParty.TinyJson;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RecordedPointsController : MonoBehaviour
{
    [SerializeField]
    private List<PointRecording> pointRecordings = new List<PointRecording>();

    [SerializeField]
    private List<RecordedPoint> recordedPoints = new List<RecordedPoint>();

    [SerializeField, Tooltip("The visual effect prefab")]
    private GameObject visualEffectPrefab;

    [SerializeField, Tooltip("The current heatpoint that has been synced.")]
    private int currentHeatPoint = 0;
    
    /// <summary>
    /// Adds an interest point to the list.
    /// </summary>
    /// <param name="raycastHit">the raycast hit</param>
    public void AddHeatmapPoint(RecordedPoint recordedPoint)
    {
        recordedPoints.Add(recordedPoint);
    }

    /// <summary>
    /// Gets the point recordings.
    /// </summary>
    /// <returns>the point recordings</returns>
    public List<PointRecording> GetPointRecordings() => pointRecordings;

    /// <summary>
    /// Syncs the heatmaps values.
    /// </summary>
    public void DeployHeatmapPoints() {
        for (int i = currentHeatPoint; i < recordedPoints.Count; i++) {
            RecordedPoint recordedPoint = recordedPoints[i];
            Transform parentTransform = recordedPoint.GetParentTransform();
            VisualDotDeployer visualDotDeployer = parentTransform.GetComponent<VisualDotDeployer>();
            if (visualDotDeployer == null)
            {
                visualDotDeployer = parentTransform.AddComponent<VisualDotDeployer>();
                visualDotDeployer.SetupVisualDot(visualEffectPrefab);
            }
            visualDotDeployer.AddHeatmapPoint(recordedPoint);
            
        }
        currentHeatPoint = recordedPoints.Count;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="raycastHit"></param>
    public void AddPointOfInterest(PointRecording pointRecording)
    {
        pointRecordings.Add(pointRecording);
        
    }

    /// <summary>
    /// Adds an order id to the last point of interest.
    /// </summary>
    /// <param name="orderId">the new order id</param>
    public bool IncrementLastPointRecording() {
        bool valid = this.pointRecordings.Count > 0;
        if (valid) {
            pointRecordings.Last().IncrementAmountOfTimes();
        }
        return valid;
    }

    /// <summary>
    /// Gets the last point recording. Returns null if the last point is not made.
    /// </summary>
    /// <returns>the point recording or null</returns>
    public PointRecording GetLastPointRecording() { 
        return pointRecordings.Count > 0 ? pointRecordings.Last() : null;
    }
}
