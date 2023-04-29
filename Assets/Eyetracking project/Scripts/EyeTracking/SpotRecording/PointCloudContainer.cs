using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PointCloudContainer : PointRecordContainer<RecordedPoint>
{
    ///<inheritdoc/>
    public PointCloudContainer(RecordedPoint recordedPoint, Transform parentTransform) : base(recordedPoint, parentTransform)
    {
    }

    public override RecordedPoint GetRecord()
    {
        return (RecordedPoint) GetRecordedPoint();
    }
}
