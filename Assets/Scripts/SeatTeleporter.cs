using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatTeleporter : MonoBehaviour {
    [SerializeField]
    [Tooltip("The collider that will be pointed at in order to teleport")]
    private Collider seatCollider;

    [SerializeField]
    [Tooltip("The player that will be teleported on activation")]
    private GameObject player;

    [SerializeField]
    [Tooltip("The canvas that will be displayed while aimed at")]
    private Canvas displayCanvas;

    [SerializeField]
    [Tooltip("The maximum amount of opacity the collider will have on selection")]
    [Range(0f,1f)]
    private float maxAlpha = 0.6f;

    private float timeToChange = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        HideCollider();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCollider() {
        StopAllCoroutines();
        GetComponent<MeshRenderer>().enabled = true;
        displayCanvas.enabled = true;
        StartCoroutine(GraduallyChangeColliderAlpha(maxAlpha, timeToChange));
    }


    public void HideCollider() {
        StopAllCoroutines();
        StartCoroutine(FadeToDisabledCollider());
        displayCanvas.enabled = false;
    }


    /*
     * Sets the alpha (opacity) of a the current material. 
     */
    private void SetMaterialAlpha(float alpha) {
        GetComponent<MeshRenderer>().enabled = true;
        Material seatMaterial = GetComponent<Renderer>().material;
        Color materialColor = seatMaterial.color;
        materialColor.a = alpha;
        seatMaterial.color = materialColor;
    }

    private IEnumerator FadeToDisabledCollider() {
        yield return StartCoroutine(GraduallyChangeColliderAlpha(0, timeToChange));
        GetComponent<MeshRenderer>().enabled = false;
    }

    private IEnumerator GraduallyChangeColliderAlpha(float targetAlpha, float time) {
        float timeElapsed = 0;
        float startingAlpha = GetComponent<Renderer>().material.color.a;
        while(timeElapsed < time) {
            SetMaterialAlpha(Mathf.Lerp(startingAlpha, targetAlpha, timeElapsed / time));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    public void TeleportToSeat() {
        player.transform.position = transform.position;
    }
}
