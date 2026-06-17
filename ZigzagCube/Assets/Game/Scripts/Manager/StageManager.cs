using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField, Tooltip("ステージ番号")]
    private int stageNumber = 1;
    [SerializeField, Tooltip("ステージ総数")]
    private int totalStages;
    public int TotalStages => totalStages;
    [SerializeField, Tooltip("ステージデータリスト")]
    private List<StageDefinition> stageList = new List<StageDefinition>();

    /// <summary>
    /// インスタンス    </summary>
    private static StageManager instance;
    public static StageManager Instance => instance;
    
    [Tooltip("現在のステージデータ")]
    public StageDefinition CurrentStage => stageList[stageNumber - 1];
    [Tooltip("現在のステージ番号")]
    public int CurrentStageNumber { get { return stageNumber; } }

    protected void Awake()
    {
        if (instance == null) instance = this;
    }

    /// <summary>
    /// ステージ番号をセット    </summary>
    /// <param name="newStageNumber">
    /// セットするステージ番号    </param>
    public void SetStage(int newStageNumber) { stageNumber = newStageNumber; }
    /// <summary>
    /// 次のステージに進む    </summary>
    public void GoToNextStage()
    {
        stageNumber++;
        if (stageNumber > stageList.Count) stageNumber = 1;
    }
    /// <summary>
    /// 前のステージに戻る    </summary>
    public void GoToPreviousStage()
    { 
        stageNumber--;
        if (stageNumber <= 0) stageNumber = stageList.Count;
    }
}
