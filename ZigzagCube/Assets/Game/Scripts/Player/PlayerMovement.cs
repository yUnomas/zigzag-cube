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
    /// 方向切り替え中かどうか    </summary>
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
        // タップで左右切り替え
        if (inputData.isTouch) TriggerDirection();

        // Z座標が戻った場合に元の位置へ戻す
        if(lastPosition.z > transform.position.z)
        {
            Debug.Log("プレイヤーのZ座標が戻りました");
            transform.position = new Vector3
                (
                    transform.position.x,
                    transform.position.y,
                    lastPosition.z
                );
        }
        // 現在のプレイヤー座標を保存
        lastPosition = transform.position;
    }
    public override void FixedExecute()
    {
        rb.linearVelocity = new Vector3(horizontalSpeed * direction + externalHorizontalSpeed, rb.linearVelocity.y, forwardSpeed);
        isChangeDirection = false;

        // 現在の値保存
        lastPosition = transform.position;
    }

    /// <summary>
    /// 方向切り替え    </summary>
    public void TriggerDirection()
    {
        if (isChangeDirection) return;

        direction *= -1;
        transform.position += Vector3.right * horizontalSpeed * direction * Time.deltaTime;
        isChangeDirection = true;

        // エフェクト・SEの再生
        Vector3 position = transform.position + -transform.forward;
        Instantiate(changeDirectionEffect, position, Quaternion.identity);
        AudioManager.Instance.PlaySE("PlayerChangeDirection");
    }
    public void AddSpeed(float value) { externalHorizontalSpeed += value; }
    public void RemoveSpeed(float value) { externalHorizontalSpeed -= value; }
}