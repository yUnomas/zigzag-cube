using UnityEngine;

public class PlayerRevive : ModuleBase<PlayerController>
{
    private FieldManager fieldManager;
    private GameplayUIController gameplayUI;

    private void Awake()
    {
        fieldManager = FindAnyObjectByType<FieldManager>();
        gameplayUI = FindAnyObjectByType<GameplayUIController>();
    }
    public void Revive()
    {
        // 安全なチャンクに復活
        Vector3 spawnPoint = fieldManager.PrepareRevivePoint();
        transform.position = spawnPoint;
        // カウントダウン後、プレイヤーを生存状態へ遷移
        gameplayUI.StartCountDown(3, () => controller.ChangeState(PlayerState.Alive));
    }
}
