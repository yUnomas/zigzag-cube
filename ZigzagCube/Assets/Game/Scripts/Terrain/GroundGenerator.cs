using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    [SerializeField, Tooltip("初回生成の切り替え")]
    private bool isGenerateAtStart = true;

    /// <summary>
    /// 生成の有無    </summary>
    private bool isGenerate;

    private void Awake()
    {
        isGenerate = isGenerateAtStart;
    }

    public GroundData[] Generate(int chunkWidth, int cellCount)
    {
        GroundData[] groundDatas = new GroundData[cellCount];
        if (!isGenerate)
        {
            for (int i = 0; i < groundDatas.Length; i++)
            {
                groundDatas[i] = new GroundData()
                {
                    type = GroundType.Default,
                    startLaneIndex = 0,
                    width = chunkWidth
                };
            }
            return groundDatas;
        }

        for (int i = 0; i < groundDatas.Length; i++)
        {
            int rand = Random.Range(0, 10);
            if (rand < 8)
            {
                groundDatas[i] = new GroundData()
                {
                    type = GroundType.Default,
                    startLaneIndex = 0,
                    width = chunkWidth
                };
            }
            else
            {
                int minWidth = (int)(chunkWidth / 1.5);
                int randWidth = Random.Range(minWidth, chunkWidth);

                groundDatas[i] = new GroundData()
                {
                    type = GroundType.Narrow,
                    startLaneIndex = Random.Range(0, chunkWidth - randWidth + 1),
                    width = randWidth
                };
            }
        }

        return groundDatas;
    }
}
