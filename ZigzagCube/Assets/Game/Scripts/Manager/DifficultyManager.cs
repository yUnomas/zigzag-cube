using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField, Tooltip("難易度上昇ごとのプレイヤーの速度上昇量")]
    private float increasePlayerSpeedPerLevel = 0.25f;
    [SerializeField, Tooltip("最初の難易度上昇に必要な距離")]
    private float initialStepDistance = 50f;
    [SerializeField, Tooltip("難易度の上昇に必要な移動距離。以降は倍増")]
    private float distanceStepIncrease = 20f;
    [SerializeField, Tooltip("必要距離の増加がストップする難易度レベル")]
    private int maxStepLevel = 5;
    [SerializeField, Tooltip("難易度の上昇がストップする難易度レベル")]
    private int maxLevel = 10;
    [Header("=====")]
    [SerializeField] private PlayerMovement player;

    public static DifficultyManager Instance => instance;
    private static DifficultyManager instance;

    /// <summary>
    /// 現在の難易度レベル    </summary>
    public int CurrentLevel => currentLevel;
    private int currentLevel = 0;
    /// <summary>
    /// 現在の難易度上昇に必要な距離    </summary>
    private float currentStepDistance;
    /// <summary>
    /// 最後にの難易度上昇した際の距離    </summary>
    private float lastIncreasedDistance;

    private void Awake()
    {
        instance = this;
        currentStepDistance = initialStepDistance;
    }
    private void Update()
    {
        if (currentLevel >= maxLevel) return;

        // 一定距離の移動で難易度を上昇
        if (player.transform.position.z - lastIncreasedDistance >= currentStepDistance)
        {
            IncreaseDifficulty();
        }
    }

    private void IncreaseDifficulty()
    {
        currentLevel++;
        Debug.Log($"難易度が{currentLevel}に上昇しました");

        // プレイヤーの速度上昇
        player.AddSpeed(increasePlayerSpeedPerLevel);

        // 次回必要な距離を設定
        float maxStepDistance = initialStepDistance + (distanceStepIncrease * maxStepLevel);
        currentStepDistance = Mathf.Min(currentStepDistance + distanceStepIncrease, maxStepDistance);
        // 今回の難易度上昇時の距離を保持
        lastIncreasedDistance = player.transform.position.z;
    }
}