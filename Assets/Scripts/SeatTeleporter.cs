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
    // Start is called before the first frame update
    void Start()
    {
        ShowCollider();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCollider() {
        SetMaterialAlpha(0.6f);
    }
    public void HideCollider() {
        SetMaterialAlpha(0f);
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void SetMaterialAlpha(float alpha) {
        GetComponent<MeshRenderer>().enabled = true;
        Material seatMaterial = GetComponent<Renderer>().material;
        seatMaterial.color = new Color(seatMaterial.color.r, seatMaterial.color.g, seatMaterial.color.b, alpha);
    }

    

    public void TeleportToSeat() {
        player.transform.position = transform.position;
    }
}
