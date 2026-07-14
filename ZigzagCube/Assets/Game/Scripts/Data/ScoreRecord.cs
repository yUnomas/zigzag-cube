using System;
using UnityEngine;

[Serializable]
public class ScoreRecord
{
    public int score;
    public string playerName;

    public ScoreRecord(int score, string playerName)
    {
        this.score = score;
        this.playerName = playerName;
    }
}