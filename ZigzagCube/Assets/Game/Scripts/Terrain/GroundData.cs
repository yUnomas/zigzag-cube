public struct GroundData
{
    public GroundType type;
    public int laneIndex;   // 地面自体のレーン番号
    public int width;

    /// <summary>
    /// 幅を考慮した際の左端レーン番号    </summary>
    public int LeftLaneIndex => laneIndex - (width - 1) / 2;
    /// <summary>
    /// 幅を考慮した際の右端レーン番号    </summary>
    public int RightLaneIndex => LeftLaneIndex + width - 1;
}