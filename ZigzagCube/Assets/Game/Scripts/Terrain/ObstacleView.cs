using System.Collections.Generic;
using UnityEngine;

public class ObstacleView : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private List<GameObject> instances = new List<GameObject>();
    /// <summary>
    /// インスタンスの使用回数    </summary>
    private int usedCount;

    private void TryAddInstance()
    {
        // インスタンスの不足分を生成
        if (instances.Count <= usedCount)
        {
            GameObject obj = Instantiate(prefab, transform);
            instances.Add(obj);
        }
    }

    protected virtual void View(int x)
    {
        // 障害物の表示と位置設定
        instances[usedCount].SetActive(true);
        Vector3 localPos = instances[usedCount].transform.localPosition;
        localPos.x = x;
        instances[usedCount].transform.localPosition = localPos;
    }

    public void Clear()
    {
        foreach (var instance in instances) { instance.SetActive(false); }
        usedCount = 0;
    }
    public void Set(int x)
    {
        TryAddInstance();
        View(x);
        usedCount++;
    }
}
