using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerLateralMove : BehaviorBase
{
    [SerializeField, Tooltip("移動速度")]
    private float baseSpeed = 1f;
    [SerializeField, Tooltip("再移動までの時間")]
    private float baseCoolTime = 0.65f;
    [SerializeField, Tooltip("再移動までの時間の減少量")]
    private float coolTimeDecreaseAmount = 1f;
    [SerializeField, Tooltip("再移動までの時間が減少する距離間隔")]
    private float coolTimeDecreasePerDistance = 100f;
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
    /// 跳ね返るかどうか    </summary>
    private bool isBounce;
    /// <summary>
    /// 再移動までの時間    </summary>
    private float coolTime;
    /// <summary>
    /// 経過時間    </summary>
    private float elapsedTime;
    /// <summary>
    /// 最後に速度が上昇した距離    </summary>
    private float lastSpeedIncreaseDirection;

    public override void Initialize()
    {
        speed = baseSpeed;
        coolTime = baseCoolTime;
        base.Initialize();
    }
    public override void Execute(InputData inputData)
    {
        // 一定距離の移動で再移動までの時間短縮
        if (transform.position.z - lastSpeedIncreaseDirection >= coolTimeDecreasePerDistance)
        {
            coolTime -= coolTimeDecreaseAmount;
            Debug.Log($"再移動までの時間:{coolTime}");
            lastSpeedIncreaseDirection = transform.position.z;
        }

        //** 左右移動
        // 一定間隔
        if (elapsedTime >= coolTime)
        {
            Move();
            elapsedTime = 0;    // 経過時間のリセット
        }
        else elapsedTime += Time.deltaTime;  // 経過時間の計測
        // タップで即時
        if (inputData.isTouch) Move();
    }

    /// <summary>
    /// 移動処理    </summary>
    private void Move()
    {
        // 跳ね返りタイミングでは、移動させない
        if (isChangeDirection && isBounce)
        {
            isBounce = false;
        }
        else
        {
            rb.MovePosition(rb.position + Vector3.right * direction);
            isChangeDirection = false;
        }
    }
    /// <summary>
    /// 方向切り替え    </summary>
    public void TriggerDirection()
    {
        if (isChangeDirection) return;

        direction *= -1;
        isChangeDirection = true;
        isBounce = true;
    }
}
