using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPressAnimation : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private GameObject buttonBase;

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonBase.SetActive(false);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonBase.SetActive(true);
    }
}
