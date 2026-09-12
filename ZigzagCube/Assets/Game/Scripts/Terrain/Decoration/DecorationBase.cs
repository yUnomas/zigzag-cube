using UnityEngine;

public class DecorationBase : StageObjectBase
{
    public DecorationType type { get; private set; }

    protected virtual void SetTransform(Transform cell, DecorationData data)
    {
        // セル配下に配置
        transform.parent = cell.transform;
        // Transform設定
        transform.localPosition = new Vector3(data.lane, data.height, 0);
        transform.localRotation = data.rotation;
    }
    public virtual void Set(Transform cell, DecorationData data)
    {
        type = data.type;
        SetTransform(cell, data);
    }
}
