public enum GroundType
{
    None,
    Ground,
    Bridge,
    MovingBridge,
    Conveyor,

    /// <summary>
    /// 前セルによって占有されている    </summary>
    Occupied = 50,
}