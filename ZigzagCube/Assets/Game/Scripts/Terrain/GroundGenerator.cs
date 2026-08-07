using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    public GroundData[] Generate(ChunkType chunkType, int chunkWidth, int cellCount)
    {
        // セル数の地面データ作成
        GroundData[] groundDatas = new GroundData[cellCount];
        
        //** 地面データの生成処理
        for (int i = 0; i < groundDatas.Length; i++)
        {
            int rand = 0;
            if (chunkType != ChunkType.Normal) rand = Random.Range(0, 10);

            if (rand < 8)
            {
                groundDatas[i] = new GroundData()
                {
                    type = GroundType.Normal,
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
                    type = GroundType.Bridge,
                    startLaneIndex = Random.Range(0, chunkWidth - randWidth + 1),
                    width = randWidth
                };
            }
        }

        return groundDatas;
    }
}
