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
        //** 移動処理
        // 一定時間経過後に左右移動
        if (elapsedTime >= coolTime)
        {
            MoveLateral();
            elapsedTime = 0;    // 経過時間のリセット
        }
        else
        {
            // タッチで左右移動の方向切り替え
            if (inputData.isTouch)
            {
                Debug.Log("タッチされた");
                lateralDirection *= -1f;
            }
            elapsedTime += Time.deltaTime;  // 経過時間の増加
        }
        // 常に前方移動
        MoveForward();
    }

    /// <summary>
    /// 前方移動    </summary>
    private void MoveForward()
    {
        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, forwardSpeed);
    }
    /// <summary>
    /// 左右移動    </summary>
    private void MoveLateral()
    {
        // 現在座標に移動量を加算
        transform.position += new Vector3(lateralDirection * lateralSpeed, 0);
    }
}