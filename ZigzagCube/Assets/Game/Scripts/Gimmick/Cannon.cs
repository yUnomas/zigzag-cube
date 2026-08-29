using UnityEngine;

public class Cannon : StageObjectBase
{
    [SerializeField, Tooltip("発射間隔")]
    private float duration = 5f;
    [SerializeField, Tooltip("発射アニメーション時間")]
    private float preDuration = 1f;
    [Header("=====")]
    [SerializeField] private Bullet[] bullets;
    [SerializeField] private Animation fireAnimation;
    [SerializeField] private EffectController cannonFireFX;

    /// <summary>
    /// 経過時間    </summary>
    private float elapsedTime;
    /// <summary>
    /// 発射したかどうか    </summary>
    private bool isFired;

    private void Update()
    {
        // 発射間隔に合わせてアニメーション実行
        if (!isFired && elapsedTime >= duration - preDuration)
        {
            fireAnimation.Play();
            isFired = true;
        }
        // 発射間隔のリセット
        else if(elapsedTime >= duration)
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
        AudioManager.Instance.PlaySE("CannonFire", false);
        // 砲弾のセット
        foreach (Bullet bullet in bullets)
        {
            if(!bullet.gameObject.activeSelf)
            {
                bullet.Set(this);
                return;
            }
        }
        Debug.LogWarning("砲弾が足りません");   // 砲弾が足りない場合の確認用ログ
    }
    public override void Clear()
    {
        base.Clear();
        elapsedTime = 0f;
    }
}