using Oculus.Installer.ThirdParty.TinyJson;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Holds all of the recorded points.
/// </summary>
public class RecordedPointsController : MonoBehaviour
{
    [SerializeField, Tooltip("The point of interest recordings")]
    private List<PointOfInterestContainer> pointRecordings = new List<PointOfInterestContainer>();

    [SerializeField, Tooltip("The point cloud recorded points")]
    private List<PointCloudContainer> recordedPoints = new List<PointCloudContainer>();

    [SerializeField, Tooltip("The visual effect prefab")]
    private GameObject visualEffectPrefab;

    [SerializeField, Tooltip("The current heatpoint that has been synced.")]
    private int currentHeatPoint = 0;
    
    /// <summary>
    /// Addsa  heatmap point.
    /// </summary>
    /// <param name="recordedPoint">the new recording</param>
    /// <param name="parentTransform">the transform that the recording hit</param>
    public void AddHeatmapPoint(RecordedPoint recordedPoint, Transform parentTransform)
    {
        recordedPoints.Add(new PointCloudContainer(recordedPoint, parentTransform));
    }

    /// <summary>
    /// Gets the point recordings.
    /// </summary>
    /// <returns>the point recordings</returns>
    public List<PointOfInterestContainer> GetPointRecordings() => pointRecordings;

    /// <summary>
    /// Syncs the heatmaps values.
    /// </summary>
    public void DeployHeatmapPoints() {
        for (int i = currentHeatPoint; i < recordedPoints.Count; i++) {
            PointCloudContainer pointCloudContainer = recordedPoints[i];
            Transform parentTransform = pointCloudContainer.GetParentTransform();
            VisualDotDeployerController visualDotDeployer = parentTransform.GetComponent<VisualDotDeployerController>();
            if (visualDotDeployer == null)
            {
                visualDotDeployer = parentTransform.AddComponent<VisualDotDeployerController>();
                visualDotDeployer.SetupVisualDot(visualEffectPrefab);
            }
            visualDotDeployer.AddHeatmapPoint(pointCloudContainer.GetRecord());
            
        }
        currentHeatPoint = recordedPoints.Count;
    }

    /// <summary>
    /// Adds a point of interest to the collection.
    /// </summary>
    /// <param name="parentTransform">the transform that was hit</param>
    /// <param name="pointRecording">the point of interest</param>
    public void AddPointOfInterest(PointRecording pointRecording, Transform parentTransform)
    {
        pointRecordings.Add(new PointOfInterestContainer(pointRecording, parentTransform));
        
    }

    /// <summary>
    /// Adds an order id to the last point of interest.
    /// </summary>
    /// <param name="orderId">the new order id</param>
    public bool IncrementLastPointRecording() {
        bool valid = this.pointRecordings.Count > 0;
        if (valid) {
            pointRecordings.Last().GetRecord().IncrementAmountOfTimes();
        }
        return valid;
    }

    /// <summary>
    /// Gets the last point recording. Returns null if the last point is not made.
    /// </summary>
    /// <returns>the point recording or null</returns>
    public PointRecording GetLastPointRecording() { 
        return pointRecordings.Count > 0 ? pointRecordings.Last().GetRecord() : null;
    }
}
