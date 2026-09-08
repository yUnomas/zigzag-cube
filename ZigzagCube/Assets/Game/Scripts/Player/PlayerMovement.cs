using UnityEngine;

public class PlayerMovement : ModuleBase<PlayerController>
{
    [SerializeField, Tooltip("前方への速度")]
    private float forwardSpeed = 1f;
    [SerializeField, Tooltip("左右への移動速度")]
    private float horizontalSpeed = 1f;
    [Header("=====")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private EffectController directionChangeFX;
    [SerializeField] private GameObject moveIndicateAnimation;

    /// <summary>
    /// 左右への加速度    </summary>
    private float externalHorizontalSpeed;
    /// <summary>
    /// 移動方向    </summary>
    private float direction = 1f;

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
                    Vector3 position = transform.position + -Vector3.forward / 2 + transform.right / 2 * -direction;
                    Quaternion rotation = direction == 1 ? Quaternion.Euler(0f, 45f, 0f) : Quaternion.Euler(0f, -45f, 0f);
                    directionChangeFX.Play(position, rotation);
                }
                break;
        }
        AudioManager.Instance.PlaySE("PlayerChangeDirection");
    }

    public void AddSpeed(float value)
    {
        forwardSpeed += value;
        horizontalSpeed += value;
    }
    public void RemoveSpeed(float value)
    {
        forwardSpeed -= value;
        horizontalSpeed -= value;
    }
    public void AddExternalSpeed(float value) { externalHorizontalSpeed += value; }
    public void RemoveExternalSpeed(float value) { externalHorizontalSpeed -= value; }

}