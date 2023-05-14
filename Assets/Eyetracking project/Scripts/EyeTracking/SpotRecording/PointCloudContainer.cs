using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the recorded point.
/// </summary>
[Serializable]
public class PointCloudContainer : PointRecordContainer<RecordedPoint>
{
    ///<inheritdoc/>
    public PointCloudContainer(RecordedPoint recordedPoint, Transform parentTransform) : base(recordedPoint, parentTransform)
    {
    }

    ///<inheritdoc/>
    public override RecordedPoint GetRecord()
    {
        return (RecordedPoint) GetRecordedPoint();
    }
}
