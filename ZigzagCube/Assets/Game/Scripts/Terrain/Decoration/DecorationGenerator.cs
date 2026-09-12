using UnityEngine;

public class DecorationGenerator : MonoBehaviour
{
    [SerializeField, Tooltip("生成数")]
    private int generateCount = 5;

    private DecorationType GetDecorationType()
    {
        return (DecorationType)Random.Range(1, (int)DecorationType.Max);
    }
    private DecorationData CreateData(int cell, int lane)
    {
        return new DecorationData()
        {
            type = GetDecorationType(),
            cell = cell,
            lane = lane,
            height = 1,
            rotation = Quaternion.Euler(0, 90 * Random.Range(0, 4), 0)
        };
    }

    public DecorationData[] Generate(int chunkWidth, int chunkLength, GroundData[] groundDatas)
    {
        // 合計セル数の配列作成
        DecorationData[] decorationDatas = new DecorationData[chunkLength];
        for (int i = 0; i < generateCount; i++)
        {
            // セルの選定
            int cell;
            while (true)
            {
                cell = Random.Range(0, chunkLength);
                if (groundDatas[cell].type == GroundType.Ground) break;
            }
            // レーンの選定
            int lane;
            while(true)
            {
                lane = Random.Range(0, chunkWidth);
                foreach(var decorationData in decorationDatas)
                {
                    if(decorationData.cell == cell && decorationData.lane == lane) continue;
                }

                if (Input.GetKeyDown(KeyCode.Return)) break;

                break;
            }

            // ギミックデータの作成
            decorationDatas[cell] = CreateData(cell, lane);
        }

        return decorationDatas;
    }
}