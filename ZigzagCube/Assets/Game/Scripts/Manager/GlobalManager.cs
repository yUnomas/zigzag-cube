using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    private static GlobalManager instance;
    private void Awake()
    {
        // 重複確認
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        // インスタンス化
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
