using UnityEngine;

public class PlayerRevive : ModuleBase<PlayerController>
{
    private FieldManager fieldManager;

    private void Awake()
    {
        fieldManager = FindAnyObjectByType<FieldManager>();
    }
    public void Revive()
    {
        // 安全なチャンクに復活
        Vector3 spawnPoint = fieldManager.PrepareRevivePoint();
        transform.position = spawnPoint;
    }
}
