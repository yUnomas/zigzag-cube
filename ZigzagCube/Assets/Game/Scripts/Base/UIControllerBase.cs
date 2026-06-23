using UnityEngine;

public abstract class UIControllerBase : MonoBehaviour
{
    [SerializeField]
    private GameObject content;

    /// <summary>
    /// UI表示    </summary>
    public void Show() { content.SetActive(true); }
    /// <summary>
    /// UI非表示    </summary>
    public void Hide() { content.SetActive(false); }
}
