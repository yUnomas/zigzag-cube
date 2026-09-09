using System;
using UnityEngine;

[Serializable]
public struct ChunkWeightEntry : IWeighted
{
    public ChunkType type;
    [SerializeField, Range(1, 100)] public int weight;
    public int Weight => weight;
}
