using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Overlay : MonoBehaviour
{
    /// konseptuell klasse. In construction.
    public abstract void ShowOverlay();

    public abstract void HideOverlay();
}
