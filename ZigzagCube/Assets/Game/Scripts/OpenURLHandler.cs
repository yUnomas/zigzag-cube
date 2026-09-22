using UnityEngine;

public class OpenURLHandler : MonoBehaviour
{
    [SerializeField, Tooltip("開くページのURLを設定")] private string url;

    public void OnClickButton()
    {
        if (!string.IsNullOrEmpty(url))
        {
            Application.OpenURL(url);
        }
    }
}
