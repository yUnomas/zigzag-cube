using UnityEngine;

public struct GroundData
{
    // 共通
    public GroundType type;
    public int startLane;   // 一番左のレーン番号
    public int width;       // X軸方向のレーン数
    public int length;      // Z軸方向のセル数
    public int height;      // Y座標の高さ
    // コンベヤー
    public int direction;

    /// <summary>
    /// 中心座標の取得    </summary>
    public Vector3 GetCenter()
    {
        return new Vector3(
            startLane + (width - 1) / 2f,
            height,
            (length - 1) / 2f);
    }
}

/*
Scale（大きさ）: Vector3(cellCount * cellWidth, height, depth)
Position（位置）:
中心セル（インデックス） = startCellIndex + (cellCount - 1) / 2.0f
X座標 = 中心セル * cellWidth
*/