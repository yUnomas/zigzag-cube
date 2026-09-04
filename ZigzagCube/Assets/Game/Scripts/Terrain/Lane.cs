using UnityEngine;

public class Lane : StageObjectBase
{
    [SerializeField, Tooltip("進行速度")]
    private float speed = 1f;
    [Header("=====")]
    [SerializeField] private Transform leftEndPoint;
    [SerializeField] private Transform rightEndPoint;
    [SerializeField] private GameObject model;

    /// <summary>
    /// レーン上を移動させる対象    </summary>
    private Transform target;
    /// <summary>
    /// 移動方向    </summary>
    private Vector3 direction = Vector3.right;
    /// <summary>
    /// 切り替え判定用のオフセット    </summary>
    private const float offset = 0.5f;

    private void Update()
    {
        target.position += speed * direction * Time.deltaTime;
        TryTurn();
    }

    /// <summary>
    /// 折り返せるなら折り返す    </summary>
    private void TryTurn()
    {
        // レーンの左端に到達したら右方向へ折り返し
        if (leftEndPoint.position.x >= target.position.x - target.localScale.x / 2 + offset)
        {
            // 左端に配置
            direction = Vector3.right;
            Vector3 pos = target.position;
            pos.x = leftEndPoint.position.x + target.localScale.x / 2 - offset;
            target.position = pos;
        }
        // レーンの右端に到達したら左方向へ折り返し
        else if (rightEndPoint.position.x <= target.position.x + target.localScale.x / 2 - offset)
        {
            direction = Vector3.left;
            // 右端に配置
            Vector3 pos = target.position;
            pos.x = rightEndPoint.position.x - target.localScale.x / 2 + offset;
            target.position = pos;
        }
    }
    public void Set(Transform cell, Transform target, int direction)
    {
        this.target = target;
        this.direction = direction == 1 ? Vector3.right : Vector3.left;
        // セル配下に配置
        transform.parent = cell.transform;
        // Transform設定
        transform.position = new Vector3(5, target.position.y, target.position.z);
    }
}
