using UnityEngine;

public class GimmickBase : StageObjectBase
{
    public GimmickType type { get; private set; }

    protected virtual void SetTransform(Transform cell, GimmickData data)
    {
        // セル配下に配置
        transform.parent = cell.transform;
        // Transform設定
        transform.localPosition = new Vector3(data.lane, data.height, 0);
    }
    public virtual void Set(Transform cell, GimmickData data, DifficultyParameterEntry param)
    {
        gameObject.SetActive(true);
        type = data.type;
        SetTransform(cell, data);
    }
}
