using System;
using UnityEngine;

[Serializable]
public struct DifficultyParameterEntry
{
    public int level;

    [Header("プレイヤー設定")]
    public float playerMoveSpeed;

    [Header("地形・ギミック速度設定")]
    public float movingBridgeSpeed;
    public float conveyorSpeed;
    public float spikeLaneSpeed;

    [Header("大砲ギミック設定")]
    public float cannonInterval;
    public float bulletSpeed;
}
