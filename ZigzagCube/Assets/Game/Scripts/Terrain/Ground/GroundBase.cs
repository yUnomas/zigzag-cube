using UnityEngine;

public class GroundBase : StageObjectBase
{
    public GroundType type { get; private set; }

    protected virtual void SetTransform(Transform cell, GroundData data)
    {
        // セル配下に配置
        transform.parent = cell.transform;
        // Transform設定
        transform.localPosition = data.GetCenter();
        transform.localScale = new Vector3(data.width, 1, data.length);
    }
    public virtual void Set(Transform cell, GroundData data, DifficultyParameterEntry param)
    {
        gameObject.SetActive(true);
        type = data.type;
        SetTransform(cell, data);
    }
}
