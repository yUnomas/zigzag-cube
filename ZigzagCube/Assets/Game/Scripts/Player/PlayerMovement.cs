using UnityEngine;

public class PlayerMovement : BehaviorBase
{
    [SerializeField, Tooltip("移動速度")]
    private float baseSpeed = 1f;
    [SerializeField, Tooltip("速度の上昇量")]
    private float speedIncreaseAmount = 1f;
    [SerializeField, Tooltip("速度が上昇する距離間隔")]
    private float speedIncreasePerDistance = 100f;
    [Header("=====")]
    [SerializeField]
    private Rigidbody rb;

    /// <summary>
    /// 速度    </summary>
    private float speed;
    /// <summary>
    /// 移動方向    </summary>
    private float direction = 1f;
    /// <summary>
    /// 方向切り替え中かどうか    </summary>
    private bool isChangeDirection;
    /// <summary>
    /// 最後に速度が上昇した距離    </summary>
    private float lastSpeedIncreaseDirection;
    /// <summary>
    /// 状態可否    </summary>
    private bool isActive;

    private void FixedUpdate()
    {
        // プレイヤーの行動可能な状態の際に移動処理
        if (isActive)
        {
            rb.linearVelocity = new Vector3(speed * direction, rb.linearVelocity.y, speed);
            isChangeDirection = false;
        }
    }

    public override void Initialize()
    {
        speed = baseSpeed;
        isActive = true;
        base.Initialize();
    }
    public override void Uninitialize()
    {
        rb.linearVelocity = Vector3.zero;
        isActive = false;
        base.Uninitialize();
    }
    public override void Execute(InputData inputData)
    {
        // 一定距離の移動で速度上昇
        if (transform.position.z - lastSpeedIncreaseDirection >= speedIncreasePerDistance)
        {
            speed += speedIncreaseAmount;
            Debug.Log($"現在の移動速度:{speed}");
            lastSpeedIncreaseDirection = transform.position.z;
        }
        // タップで左右切り替え
        if (inputData.isTouch) TriggerDirection();
    }

    /// <summary>
    /// 方向切り替え    </summary>
    public void TriggerDirection()
    {
        if (isChangeDirection) return;

        direction *= -1;
        isChangeDirection = true;
    }
}