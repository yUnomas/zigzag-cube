using UnityEngine;

public class PlayerData
{
    public string name;
    public string id;

    /// <summary>
    /// ランダムなプレイヤー名の生成    </summary>
    private string GenerateRandomName()
    {
        string randomName = "";
        string[] playerList = { "PLAYER", "ENDLESS", "RUNNER", "ZIGZAG", "CUBE" };
        string[] symbolList = { "", "_", "#", "." };

        randomName += playerList[Random.Range(0, playerList.Length)];
        randomName += symbolList[Random.Range(0, symbolList.Length)];
        randomName += Random.Range(0, 10000).ToString("D4");

        Debug.Log(randomName);
        return randomName;
    }
    public PlayerData()
    {
        name = GenerateRandomName();
        id = System.Guid.NewGuid().ToString();
    }
}
