using System.Collections.Generic;
using UnityEngine;

public class ObstacleView : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private GameObject prefab;

    private List<GameObject> instances = new List<GameObject>();
    private int usedCount;

    private void TryAddInstance()
    {
        // インスタンスの不足分を生成
        if (instances.Count <= usedCount)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.parent = root.transform;
            instances.Add(obj);
        }
    }

    protected virtual void View(int x)
    {
        // 障害物の表示と位置設定
        instances[usedCount].SetActive(true);
        instances[usedCount].transform.localPosition = new Vector3(x, 1, 0);
        usedCount++;
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
    }
}
