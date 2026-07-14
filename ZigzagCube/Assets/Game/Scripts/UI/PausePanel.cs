using UnityEngine;
using UnityEngine.EventSystems;

public class PausePanel : MonoBehaviour, IPointerClickHandler
{
    GameplayUIController gameplayUI;

    private void Awake()
    {
        gameplayUI = FindAnyObjectByType<GameplayUIController>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        gameplayUI.OnClickPausePanel();
    }
}
