using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains a point of interest.
/// </summary>
[Serializable]
public class PointOfInterestContainer : PointRecordContainer<PointRecording>
{
    ///<inheritdoc/>
    public PointOfInterestContainer(PointRecording recordedPoint, Transform parentTransform) : base(recordedPoint, parentTransform)
    {
    }

    ///<inheritdoc/>
    public override PointRecording GetRecord()
    {
        return (PointRecording) GetRecordedPoint();
    }
}
