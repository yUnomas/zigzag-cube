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
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private GameObject changeDirectionEffect;
    [SerializeField] private GameObject moveIndicateAnimation;

    private float externalHorizontalSpeed;
    /// <summary>
    /// 移動方向    </summary>
    private float direction = 1f;
    /// <summary>
    /// 最後に速度が上昇した距離    </summary>
    private float lastSpeedIncreaseDirection;

    public override void Activate()
    {
        rb.useGravity = true;

        if(moveIndicateAnimation.activeSelf) moveIndicateAnimation.SetActive(false);
    }
    public override void Deactivate()
    {
        rb.linearVelocity = Vector3.zero;
        rb.useGravity = false;
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
        if (inputData.isTouch) ChangeDirection();

        // 地面から落下した場合に方向切り替えを無効化
        if (transform.position.y < 1.0f && !boxCollider.enabled)
        {
            Debug.Log($"プレイヤーが地面から落下しました \n Y:{transform.position.y}");
            boxCollider.enabled = true;
        }
        else if (boxCollider.enabled && transform.position.y >= 1.0f)
        {
            Debug.Log($"プレイヤーが落下から復帰しました \n Y:{transform.position.y}");
            boxCollider.enabled = false;
        }

    }
    public override void FixedExecute()
    {
        rb.linearVelocity = new Vector3(
            horizontalSpeed * direction + externalHorizontalSpeed,
            rb.linearVelocity.y,
            forwardSpeed);
    }

    /// <summary>
    /// 方向切り替え    </summary>
    public void ChangeDirection()
    {
        direction *= -1;

        // プレイヤーの状態による分岐
        switch(controller.State)
        {
            case PlayerState.Idle:
                {
                    // 進行方向を示すアニメーションの方向切り替え
                    Vector3 scale = moveIndicateAnimation.transform.localScale;
                    scale.x = direction;
                    moveIndicateAnimation.transform.localScale = scale;
                }
                break;
            case PlayerState.Alive:
                {
                    // エフェクト再生
                    Vector3 position = transform.position + -transform.forward;
                    Instantiate(changeDirectionEffect, position, Quaternion.identity);
                }
                break;
        }
        AudioManager.Instance.PlaySE("PlayerChangeDirection");
    }
    public void AddSpeed(float value) { externalHorizontalSpeed += value; }
    public void RemoveSpeed(float value) { externalHorizontalSpeed -= value; }
}