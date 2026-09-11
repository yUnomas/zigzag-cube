using UnityEngine;

public class PlayerRevive : ModuleBase<PlayerController>
{
    [SerializeField] private EffectController shatterFX;
    [SerializeField] private GameplayUIController gameplayUI;

    private void Awake()
    {
        if(gameplayUI == null) gameplayUI = FindAnyObjectByType<GameplayUIController>();
    }
    public void Revive()
    {
        shatterFX.Clear();
        // 安全なチャンクに復活
        Vector3 spawnPoint = FieldManager.Instance.PrepareRevivePoint();
        transform.position = spawnPoint;
        // カウントダウン後、プレイヤーを生存状態へ遷移
        gameplayUI.StartCountDown(3, () => controller.ChangeState(PlayerState.Alive));
    }
}