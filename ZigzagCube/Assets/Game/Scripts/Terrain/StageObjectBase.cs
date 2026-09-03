using UnityEngine;

public class StageObjectBase : MonoBehaviour
{
    protected virtual void SetTransform(Transform cell, int laneIndex, int y, float width)
    {
        float x = laneIndex + (width - 1) / 2;
        // Transform設定
        transform.parent = cell.transform;
        Vector3 pos = cell.position;
        pos.x = x;
        pos.y = y;
        transform.position = pos;
        transform.localScale = new Vector3(width, 1, 1);
    }

    public virtual void Clear()
    {
        gameObject.SetActive(false);
    }
    public virtual void Set(Transform cell, int laneIndex, int y, float width)
    {
        gameObject.SetActive(true);
        SetTransform(cell, laneIndex, y, width);
    }
}