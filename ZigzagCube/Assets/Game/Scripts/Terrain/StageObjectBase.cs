using UnityEngine;

public class StageObjectBase : MonoBehaviour
{
    public virtual void Clear()
    {
        gameObject.SetActive(false);
    }
    public virtual void Set(int startLaneIndex, float width)
    {
        float x = startLaneIndex + (width - 1) / 2;
        // 地面の表示とサイズ設定
        gameObject.SetActive(true);
        Vector3 pos = transform.localPosition;
        pos.x = x;
        transform.localPosition = pos;
        transform.localScale = new Vector3(width, 1, 1);
    }
}