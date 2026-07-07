using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPressAnimation : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private GameObject idleButton;

    public void OnPointerDown(PointerEventData eventData)
    {
        idleButton.SetActive(false);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        idleButton.SetActive(true);
    }
}
