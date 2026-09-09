using System;

[Serializable]
public struct LevelChunkGroup
{
    public int level;
    public ChunkWeightEntry[] entries;
}