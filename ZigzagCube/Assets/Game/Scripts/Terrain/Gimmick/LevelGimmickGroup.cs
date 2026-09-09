using System;

[Serializable]
public struct LevelGimmickGroup
{
    public int level;
    public int generateCount;
    public GimmickWeightEntry[] entries;
}