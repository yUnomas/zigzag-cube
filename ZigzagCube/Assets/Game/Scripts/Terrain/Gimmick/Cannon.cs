using UnityEngine;

public class Cannon : StageObjectBase
{
    [SerializeField, Tooltip("発射間隔")]
    private float interval = 5f;
    [SerializeField, Tooltip("発射アニメーション時間")]
    private float fireAnimTiming = 1f;
    [Header("=====")]
    [SerializeField] private Animation fireAnimation;
    [SerializeField] private EffectController cannonFireFX;

    private BulletPool pool;
    /// <summary>
    /// 経過時間    </summary>
    private float elapsedTime;
    /// <summary>
    /// 発射したかどうか    </summary>
    private bool isFired;
    /// <summary>
    /// 砲弾の速度    </summary>
    private float bulletSpeed;

    private void Awake()
    {
        pool = FindAnyObjectByType<BulletPool>();
    }
    private void Update()
    {
        // 発射間隔に合わせてアニメーション実行
        if (!isFired && elapsedTime >= interval - fireAnimTiming)
        {
            fireAnimation.Play();
            isFired = true;
        }
        // 発射間隔のリセット
        else if(elapsedTime >= interval)
        {
            Fire();
            elapsedTime = 0f;
            isFired = false;
        }
        else elapsedTime += Time.deltaTime;
    }

    private void Fire()
    {
        // エフェクト・SE再生
        cannonFireFX.Play();
        AudioManager.Instance.PlaySE("CannonFire", transform.position);
        // 砲弾のセット
        Bullet bullet = pool.Get();
        bullet.transform.SetPositionAndRotation(
                transform.position,
                transform.rotation
            );
        bullet.Set(this, pool, bulletSpeed);
    }
    public override void Set(Transform cell, GimmickData data, DifficultyParameterEntry param)
    {
        elapsedTime = 0f;
        interval = param.cannonInterval;
        bulletSpeed = param.bulletSpeed;
        base.Set(cell, data, param);
    }
}