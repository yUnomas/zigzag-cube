using UnityEngine;

public class PlayerForwardMove : BehaviorBase
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
    /// 現在の速度    </summary>
    private float speed;
    /// <summary>
    /// 最後に速度が上昇した距離    </summary>
    private float lastSpeedIncreaseDirection;

    public override void Initialize()
    {
        speed = baseSpeed;
    }
    public override void Uninitialize()
    {
        rb.linearVelocity = Vector3.zero;
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
        // 前方移動
        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, speed);
    }
}
