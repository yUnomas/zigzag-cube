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
    private GameObject target;
    /// <summary>
    /// 移動方向    </summary>
    private Vector3 direction = Vector3.right;
    /// <summary>
    /// 切り替え判定用のオフセット    </summary>
    private const float offset = 0.5f;

    private void Update()
    {
        target.transform.position += speed * direction * Time.deltaTime;
        TryTurn();
    }

    /// <summary>
    /// 折り返せるなら折り返す    </summary>
    private void TryTurn()
    {
        // レーンの左端に到達したら右方向へ折り返し
        if (leftEndPoint.position.x >= target.transform.position.x - target.transform.localScale.x / 2 + offset)
        {
            // 左端に配置
            direction = Vector3.right;
            Vector3 pos = target.transform.position;
            pos.x = leftEndPoint.position.x + target.transform.localScale.x / 2 - offset;
            target.transform.position = pos;
        }
        // レーンの右端に到達したら左方向へ折り返し
        else if (rightEndPoint.position.x <= target.transform.position.x + target.transform.localScale.x / 2 - offset)
        {
            direction = Vector3.left;
            // 右端に配置
            Vector3 pos = target.transform.position;
            pos.x = rightEndPoint.position.x - target.transform.localScale.x / 2 + offset;
            target.transform.position = pos;
        }
    }
    public void Set(GameObject target, int direction, bool isVisible)
    {
        gameObject.SetActive(true);
        model.SetActive(isVisible);
        this.target = target;
        this.direction = direction == 1 ? Vector3.right : Vector3.left;
        // Y座標を対象に合わせる
        Vector3 pos = transform.localPosition;
        pos.y = target.transform.localPosition.y;
        transform.localPosition = pos;
    }
}
