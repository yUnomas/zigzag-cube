using UnityEngine;

public abstract class UIControllerBase : MonoBehaviour
{
    [SerializeField] private GameObject visibleTarget;

    /// <summary>
    /// UI表示    </summary>
    public virtual void Show() { visibleTarget.SetActive(true); }
    /// <summary>
    /// UI非表示    </summary>
    public virtual void Hide() { visibleTarget.SetActive(false); }
    /// <summary>
    /// UI表示切り替え    </summary>
    public void Toggle()
    {
        if (visibleTarget.activeSelf) Hide();
        else Show();
    }
}
