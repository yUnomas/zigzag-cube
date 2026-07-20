using GoogleMobileAds.Api;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance => instance;
    private static AdsManager instance;

#if UNITY_IOS
    private const string InterstitialAdUnitId =
        "ca-app-pub-3940256099942544/4411468910";
#elif UNITY_ANDROID
    private const string InterstitialAdUnitId =
        "ca-app-pub-3940256099942544/1033173712";
#else
    private const string InterstitialAdUnitId = "unused";
#endif

    private void Awake()
    {
        // インスタンス化
        if (instance == null) instance = this;
    }
    private void Start()
    {
        Initialize();
    }

    /// <summary>
    /// 初期化    </summary>
    private void Initialize()
    {
        MobileAds.Initialize((InitializationStatus initstatus) =>
        {
            if (initstatus == null)
            {
                Debug.LogError("Google Mobile Adsの初期化に失敗");
                return;
            }

            Debug.Log("Google Mobile Adsの初期化が完了");
        });
    }
}
