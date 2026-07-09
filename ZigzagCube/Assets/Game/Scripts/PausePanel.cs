using UnityEngine;
using UnityEngine.EventSystems;

public class PausePanel : MonoBehaviour, IPointerClickHandler
{
    GameplayPopupController gameplayPopup;

    private void Awake()
    {
        gameplayPopup = FindAnyObjectByType<GameplayPopupController>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        gameplayPopup.OnPausePanelTapped();
    }
}
