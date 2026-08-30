using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Tooltip("弾速")]
    private float speed = 5f;
    [SerializeField, Tooltip("この距離以上プレイヤーの後ろに進むと削除")]
    private float clearDistance = 8f;
    [SerializeField, Tooltip("大砲へ戻るまでの時間（秒）")]
    private float waitDuration = 1.0f;
    [Header("=====")]
    [SerializeField] private GameObject model;
    [SerializeField] private Collider bulletCollider;
    [SerializeField] private EffectController bulletExplosionFX;

    private Cannon cannon;
    private PlayerController player;
    /// <summary>
    /// 衝突状態    </summary>
    private bool isHit;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>();
    }
    private void Update()
    {
        if (isHit) return;

        transform.position += transform.forward * speed * Time.deltaTime;
        TryClearByDistance();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isHit) return;

        // プレイヤーと衝突した際はプレイヤーを死亡
        if (collision.gameObject.TryGetComponent<PlayerDeath>(out var playerDeath))
        {
            // 衝突位置が存在しない場合は衝突対象の座標を設定
            Vector3 contactPoint = collision.contactCount > 0
            ? collision.GetContact(0).point
            : collision.transform.position;

            playerDeath.Die(DeathType.Default, contactPoint);
        }
        // 自身を発射した大砲との衝突は無視
        else if (collision.gameObject == cannon.gameObject) return;

        OnHit();
    }

    /// <summary>
    /// プレイヤーとの距離に応じて削除    </summary>
    private void TryClearByDistance()
    {
        if (player.transform.position.z - transform.position.z >= clearDistance) ReturnToCannon();
    }
    /// <summary>
    /// 大砲に戻る    </summary>
    private void ReturnToCannon()
    {
        gameObject.SetActive(false);
        transform.position = cannon.transform.position;
    }
    /// <summary>
    /// 衝突時のイベント    </summary>
    private void OnHit()
    {
        isHit = true;
        model.SetActive(false);
        bulletCollider.enabled = false;
        // エフェクトを再生し、エフェクト終了まで待機
        bulletExplosionFX.Play();
        _ = WaitAndReturnAsync();
    }
    /// <summary>
    /// 一定秒数待機後に大砲に戻る     </summary>
    private async Awaitable WaitAndReturnAsync()
    {
        await Awaitable.WaitForSecondsAsync(waitDuration);
        ReturnToCannon();
    }


    public void Set(Cannon cannon)
    {
        isHit = false;
        model.SetActive(true);
        bulletCollider.enabled = true;
        gameObject.SetActive(true);
        this.cannon = cannon;   // 自身を発射した大砲を保存
    }
}
