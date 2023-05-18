using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UNFINISHED 
/// Contains different visualizations for selection of objects.
/// </summary>
public class SelectionVisualizer : MonoBehaviour {
    [SerializeField]
    [Tooltip("The maximum amount of opacity the collider will have on selection")]
    [Range(0f, 1f)]
    private float maxAlpha = 0.6f;

    private float changeDuration = 0.3f;

    private int aimedAtAmount = 0; 
    
    // Start is called before the first frame update
    void Start() {
        HideCollider();
       }

    /// <summary>
    /// Starts making the hitbox visible, and stops other similar coroutines from continuing.
    /// </summary>
    public void ShowCollider() {
        //Missing functionality to make the seat available. Commented out for now.
        if (aimedAtAmount > 0) {
          //  GetComponent<MeshRenderer>().enabled = true;
            StartCoroutine(GraduallyChangeColliderAlpha(maxAlpha, changeDuration));
        }
    }

    /// <summary>
    /// Starts making the hitbox around a seatteleporter less visible, until it is hidden entirely. 
    /// </summary>
    public void HideCollider() {
        if (aimedAtAmount < 1) {
            StopAllCoroutines();
            StartCoroutine(FadeToDisabledCollider());
        }
    }

    /// <summary>
    /// Sets the alpha (opacity) of a the current material.
    /// </summary>
    /// <param name="alpha">A value between 0 and 1 indicating the alpha of the material, with 0 being completely transparent, and 1 being completely opaque.</param>
    private void SetMaterialAlpha(float alpha) {
        GetComponent<MeshRenderer>().enabled = true;
        Material seatMaterial = GetComponent<Renderer>().material;
        Color materialColor = seatMaterial.color;
        materialColor.a = alpha;
        seatMaterial.color = materialColor;
    }

    /// <summary>
    /// Fades away the visibility of the collider, then disables its' visibility entirely. 
    /// </summary>
    /// <returns>Returns a gradual change of alpha into transparency</returns>
    private IEnumerator FadeToDisabledCollider() {
        yield return StartCoroutine(GraduallyChangeColliderAlpha(0, changeDuration));
    }

    /// <summary>
    /// Gradually changes the alpha of the assigned colliders' material over time.
    /// </summary>
    /// <param name="targetAlpha">The alpha the material will have after the coroutine has finished</param>
    /// <param name="time">The amount of time needed to reach targetAlpha in seconds</param>
    /// <returns>null, passing the coroutine to the next update</returns>
    private IEnumerator GraduallyChangeColliderAlpha(float targetAlpha, float time) {
        float timeElapsed = 0;
        float startingAlpha = GetComponent<Renderer>().material.color.a;
        while (timeElapsed < time) {
            SetMaterialAlpha(Mathf.Lerp(startingAlpha, targetAlpha, timeElapsed / time));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    public void IncrementAimedAtAmount() { aimedAtAmount++; }
    public void DecrementAimedAtAmount() { aimedAtAmount--; }
}


