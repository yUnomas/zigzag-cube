using UnityEngine;

public class PlayerDeath : ModuleBase<PlayerController>
{
    [SerializeField, Tooltip("死亡アニメーション用の待機時間")]
    private float deathAnimationDuration = 1f;
    [Header("=====")]
    [SerializeField, Tooltip("障害物衝突時のエフェクト")]
    private GameObject obstacleHitEffect;
    [SerializeField, Tooltip("水衝突時のエフェクト")]
    private GameObject waterHitEffect;

    private const string waterTag = "Water";
    private const string obstacleTag = "Obstacle";

    private bool isDying;

    private void OnCollisionEnter(Collision collision)
    {
        // 障害物との衝突時
        if (collision.gameObject.CompareTag(obstacleTag))
        {
            _ = DeathAsync(collision.contacts[0].point, obstacleHitEffect, "PlayerBreak");
        }
        // 水との衝突時
        else if (collision.gameObject.CompareTag(waterTag))
        {
            _ = DeathAsync(collision.contacts[0].point, waterHitEffect, "WaterSplash");
        }
    }
    public override void Activate()
    {
        isDying = false;
    }
    private async Awaitable DeathAsync(Vector3 deathPoint, GameObject effect, string audioID)
    {
        if (isDying) return;

        isDying = true;
        controller.ChangeState(PlayerState.Dying);
        // エフェクト・SEの再生
        Instantiate(effect, deathPoint, Quaternion.identity);
        AudioManager.Instance.PlaySE(audioID, false);
        // 一定秒数待機
        await Awaitable.WaitForSecondsAsync(deathAnimationDuration);
        // プレイヤーを死亡状態に遷移
        controller.ChangeState(PlayerState.Death);
    }

    public void Die(Vector3 deathPoint)
    {
        _ = DeathAsync(deathPoint, obstacleHitEffect, "PlayerBreak");
    }
}