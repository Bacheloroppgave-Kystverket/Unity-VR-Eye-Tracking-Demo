using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Just an enum that has the layers as their value. This way ensuring no spelling errors will occur through the app.
/// </summary>
public enum PrefixLayer : int
{
    /// <summary>
    /// Layers that are trackable.
    /// </summary>
    Eyetracking,

    /// <summary>
    /// Layers that are overlays.
    /// </summary>
    VROverlay,
}
