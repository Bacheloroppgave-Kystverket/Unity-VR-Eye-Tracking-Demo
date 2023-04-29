using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class VisualDotDeployerController : MonoBehaviour
{
    [SerializeField]
    private VisualEffect visualEffect;

    [SerializeField, Tooltip("Set to true if the dots are supposed to stop. False otherwise.")]
    private bool stop = false;

    [SerializeField, Tooltip("The heatmap points")]
    private List<RecordedPoint> heatmapPoints = new List<RecordedPoint>();

    [SerializeField, Tooltip("The points of interests")]
    private List<PointOfInterestContainer> pointOfInterests = new List<PointOfInterestContainer>();

    public void SetupVisualDot(GameObject prefab) {
        GameObject visualEffectObject = Instantiate(prefab, transform);
        visualEffectObject.transform.SetParent(transform);
        visualEffectObject.transform.localPosition = Vector3.zero;
        visualEffectObject.transform.localScale = transform.InverseTransformVector(new Vector3(1f, 1f, 1f));
        this.visualEffect = visualEffectObject.GetComponent<VisualEffect>();
    }

    /// <summary>
    /// Adds an heatmap point to the list.
    /// </summary>
    /// <param name="recordedPoint">the recorded point</param>
    public void AddHeatmapPoint(RecordedPoint recordedPoint) {
        CheckIfObjectIsNull(recordedPoint, "recorded point");
        heatmapPoints.Add(recordedPoint);
    }

    /// <summary>
    /// Adds a point of interest.
    /// </summary>
    /// <param name="pointOfInterestContainer">the point of interest</param>
    public void AddPointOfInterest(PointOfInterestContainer pointOfInterestContainer) {
        CheckIfObjectIsNull(pointOfInterestContainer, "point of interest");
        pointOfInterests.Add(pointOfInterestContainer);
    }

    /// <summary>
    /// Shows all the heatmap points.
    /// </summary>
    /// <param name="showAsSolid">true if the heatmap points should be solid</param>
    public void ShowAllHeatmapPoints(bool showAsSolid) {
        StartCoroutine(ShowHeatmap(showAsSolid));
    }

    

    /// <summary>
    /// Shows the heatmap
    /// </summary>
    /// <param name="showAsSolid">true if the heatmap should be solid color</param>
    /// <returns></returns>
    public IEnumerator ShowHeatmap(bool showAsSolid) {
        IEnumerator<RecordedPoint> it =  heatmapPoints.GetEnumerator();
        visualEffect.SetBool("ShowParticle", true);
        visualEffect.SetBool("Heatmap", !showAsSolid);
        while (it.MoveNext()) {
            yield return new WaitForFixedUpdate();
            ShowDot(it.Current, FindVectorOfDot(it.Current));
        }
    }


    /// <summary>
    /// Shows a recorded dot.
    /// </summary>
    /// <param name="recordedPoint">the recorded dot</param>
    private void ShowDot(RecordedPoint recordedPoint, Vector3 vectorOfPoint)
    {
        this.visualEffect.SetVector3("PositionOfDot", FindVectorOfDot(recordedPoint));
        this.visualEffect.SendEvent("OnPlay");
    }

    /// <summary>
    /// Finds the vector of the dot. First to worldspace and then to the empty box that the dots are in.
    /// </summary>
    /// <param name="recordedPoint">the recorded point.</param>
    /// <returns>the position of the recorded point</returns>
    private Vector3 FindVectorOfDot(RecordedPoint recordedPoint) {
        return visualEffect.transform.InverseTransformPoint(transform.TransformPoint(recordedPoint.GetLocalPosition()));
    }

    public void RemoveVisualDots() {
        this.visualEffect.SetBool("ShowParticle", false);
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
