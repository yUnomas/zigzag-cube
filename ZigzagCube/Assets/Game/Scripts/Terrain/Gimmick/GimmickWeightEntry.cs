using System;
using UnityEngine;

[Serializable]
public class GimmickWeightEntry : IWeighted
{
    public GimmickType type;
    [SerializeField, Range(1, 100)] public int weight;
    public int Weight => weight;
}