using UnityEngine;

public class StageObjectBase : MonoBehaviour
{
    protected virtual void SetTransform(Transform cell, GroundData data)
    {
        // セル配下に配置
        transform.parent = cell.transform;
        // Transform設定
        transform.localPosition = data.GetCenter();
        transform.localScale = new Vector3(data.width, 1, data.length);
    }
    protected virtual void SetTransform(Transform cell, GimmickData data)
    {
        // セル配下に配置
        transform.parent = cell.transform;
        // Transform設定
        transform.localPosition = new Vector3(data.lane, data.height, 0);
    }

    public virtual void Set(Transform cell, GroundData data)
    {
        gameObject.SetActive(true);
        SetTransform(cell, data);
    }
    public virtual void Set(Transform cell, GimmickData data)
    {
        gameObject.SetActive(true);
        SetTransform(cell, data);
    }
}