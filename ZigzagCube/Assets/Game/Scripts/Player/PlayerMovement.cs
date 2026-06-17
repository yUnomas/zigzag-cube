using UnityEngine;

public class PlayerMovement : BehaviorBase
{
    [SerializeField, Tooltip("前方向への速度")]
    private float forwardSpeed = 1f;
    [SerializeField, Tooltip("横方向への速度")]
    private float lateralSpeed = 1f;
    [SerializeField, Tooltip("開始時の再移動までの待機時間")]
    private float startCoolTime = 0.65f;
    [Header("=====")]
    [SerializeField]
    private Rigidbody rb;

    /// <summary>
    /// 横方向の移動方向    </summary>
    private float lateralDirection = 1f;
    /// <summary>
    /// 再移動までの待機時間    </summary>
    private float coolTime;
    /// <summary>
    /// 現在の待機時間    </summary>
    private float elapsedTime;

    public override void Initialize()
    {
        coolTime = startCoolTime;
        base.Initialize();
    }
    public override void Execute(InputData inputData)
    {
        // 一定時間経過後に移動
        if (elapsedTime >= coolTime)
        {
            // 現在座標に移動量を加算
            transform.position += new Vector3(lateralDirection * lateralSpeed, 0, forwardSpeed);
            // 経過時間のリセット
            elapsedTime = 0;
        }
        else
        {
            // タッチで回転方向の切り替え
            if (inputData.isTouch)
            {
                Debug.Log("タッチされた");
                lateralDirection *= -1f;
            }
            // 経過時間の増加
            elapsedTime += Time.deltaTime;
        }
    }
}