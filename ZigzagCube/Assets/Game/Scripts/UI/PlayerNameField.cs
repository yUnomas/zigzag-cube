using TMPro;
using UnityEngine;

public class PlayerNameField : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerNameInputField;

    public void SetPlayerName(string playerName)
    {
        playerNameInputField.text = playerName;
    }
}
