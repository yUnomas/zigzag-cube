using UnityEngine;

public struct DecorationData
{
    public DecorationType type;
    public int cell;            // セル番号
    public int lane;            // レーン番号
    public int height;          // Y座標の高さ
    public Quaternion rotation; // 回転角度
}