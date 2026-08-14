using UnityEngine;

public class PlayerMovement : ModuleBase<PlayerController>
{
    [SerializeField, Tooltip("前方への速度")]
    private float forwardSpeed = 1f;
    [SerializeField, Tooltip("左右への移動速度")]
    private float horizontalSpeed = 1f;
    [SerializeField, Tooltip("速度の上昇量")]
    private float speedIncreaseAmount = 1f;
    [SerializeField, Tooltip("速度が上昇する距離間隔")]
    private float speedIncreasePerDistance = 100f;
    [Header("=====")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject changeDirectionEffect;

    private float externalHorizontalSpeed;
    /// <summary>
    /// 移動方向    </summary>
    private float direction = 1f;
    /// <summary>
    /// 方向切り替え可能状態    </summary>
    private bool isChangeDirection;
    /// <summary>
    /// 最後に速度が上昇した距離    </summary>
    private float lastSpeedIncreaseDirection;
    /// <summary>
    /// 前フレームのプレイヤー座標    </summary>
    private Vector3 lastPosition;

    public override void Activate()
    {
        rb.useGravity = true;
        lastPosition = transform.position;

        isChangeDirection = true;
    }
    public override void Deactivate()
    {
        rb.linearVelocity = Vector3.zero;
        rb.useGravity = true;
    }

    public override void Execute(InputData inputData)
    {
        // 一定距離の移動で速度上昇
        if (transform.position.z - lastSpeedIncreaseDirection >= speedIncreasePerDistance)
        {
            forwardSpeed += speedIncreaseAmount;
            horizontalSpeed += speedIncreaseAmount;
            Debug.Log($"現在の移動速度:{forwardSpeed}");
            lastSpeedIncreaseDirection = transform.position.z;
        }
        // タップで方向切り替え
        if (inputData.isTouch) TriggerDirection();

        // 地面から落下した場合に方向切り替えを無効化
        if (transform.position.y <= 0.9f)
        {
            Debug.Log("プレイヤーが地面から落下しました");
            isChangeDirection = false;
        }
        // 現在のプレイヤー座標を保存
        lastPosition = transform.position;
    }
    public override void FixedExecute()
    {
        rb.linearVelocity = new Vector3(horizontalSpeed * direction + externalHorizontalSpeed, rb.linearVelocity.y, forwardSpeed);

        // 現在の値保存
        lastPosition = transform.position;
    }

    private bool IsChangeDirection()
    {
        return isChangeDirection && transform.position.y > 0.9f;
    }
    /// <summary>
    /// 方向切り替え    </summary>
    public void TriggerDirection()
    {
        if (!IsChangeDirection()) return;

        direction *= -1;
        transform.position += Vector3.right * horizontalSpeed * direction * Time.deltaTime;

        // エフェクト・SEの再生
        Vector3 position = transform.position + -transform.forward;
        Instantiate(changeDirectionEffect, position, Quaternion.identity);
        AudioManager.Instance.PlaySE("PlayerChangeDirection");
    }
    public void AddSpeed(float value) { externalHorizontalSpeed += value; }
    public void RemoveSpeed(float value) { externalHorizontalSpeed -= value; }
}