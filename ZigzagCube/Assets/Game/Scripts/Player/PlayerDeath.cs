using UnityEngine;

public class PlayerDeath : ModuleBase<PlayerController>
{
    [SerializeField, Tooltip("死亡アニメーション用の待機時間")]
    private float deathAnimationDuration = 1f;
    [Header("=====")]
    [SerializeField, Tooltip("死亡時のキューブ崩壊エフェクト")]
    private GameObject playerBreakEffect;

    private async Awaitable DeathAsync()
    {
        // 死亡中状態へ遷移
        controller.ChangeState(PlayerState.Dying);
        // 一定秒数待機後に死亡状態へ遷移
        await Awaitable.WaitForSecondsAsync(deathAnimationDuration);
        controller.ChangeState(PlayerState.Death);
    }
    public void Die(DeathType deathType, Vector3 contactPoint)
    {
        if (controller.State != PlayerState.Alive) return;

        switch(deathType)
        {
            case DeathType.Default:
                {
                    // エフェクト・SEの再生
                    Instantiate(playerBreakEffect, contactPoint, Quaternion.identity);
                    AudioManager.Instance.PlaySE("PlayerBreak", false);
                }
                break;
        }

        _ = DeathAsync();
    }
}