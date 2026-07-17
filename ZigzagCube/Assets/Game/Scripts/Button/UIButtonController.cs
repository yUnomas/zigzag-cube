using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField, Tooltip("押下時の音声ID")]
    private string audioID;
    [SerializeField] private GameObject idleButton;

    public void OnPointerDown(PointerEventData eventData)
    {
        idleButton.SetActive(false);
        AudioManager.Instance.PlaySE(audioID);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        idleButton.SetActive(true);
    }
}
