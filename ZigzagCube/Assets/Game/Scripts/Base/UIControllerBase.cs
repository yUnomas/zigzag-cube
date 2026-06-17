using UnityEngine;

public abstract class UIControllerBase : MonoBehaviour
{
    [SerializeField]
    private GameObject content;

    /// <summary>
    /// UI•\Ž¦    </summary>
    public void Show() { content.SetActive(true); }
    /// <summary>
    /// UI”ñ•\Ž¦    </summary>
    public void Hide() { content.SetActive(false); }
}
