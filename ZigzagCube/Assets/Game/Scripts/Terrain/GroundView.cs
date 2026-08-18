using UnityEngine;

public class GroundView : MonoBehaviour
{
    [SerializeField] private GameObject ground;

    public virtual void Clear()
    {
        ground.SetActive(false);
    }
    public virtual void View(int startLaneIndex, float width)
    {
        float x = startLaneIndex + (width - 1) / 2;
        // 地面の表示とサイズ設定
        ground.SetActive(true);
        ground.transform.localPosition = new Vector3(x, 0, 0);
        ground.transform.localScale = new Vector3(width, 1, 1);
    }
}
