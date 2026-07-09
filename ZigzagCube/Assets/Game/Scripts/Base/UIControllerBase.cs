using UnityEngine;

public abstract class UIControllerBase : MonoBehaviour
{
    [SerializeField]
    private GameObject content;

    /// <summary>
    /// UI表示    </summary>
    public virtual void Show() { content.SetActive(true); }
    /// <summary>
    /// UI非表示    </summary>
    public virtual void Hide() { content.SetActive(false); }
    /// <summary>
    /// UI表示切り替え    </summary>
    public void Toggle()
    {
        if (content.activeSelf) Hide();
        else Show();
    }
}
