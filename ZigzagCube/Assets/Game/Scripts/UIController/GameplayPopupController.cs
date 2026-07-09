using UnityEngine;

public class GameplayPopupController : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;

    public void OnPause()
    {
        GameplayManager.Instance.TogglePause();
        PausePanel.SetActive(true);
    }
    public void OnPausePanelTapped()
    {
        GameplayManager.Instance.TogglePause();
        PausePanel.SetActive(false);
    }
}
