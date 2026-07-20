using GoogleMobileAds.Api;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    [SerializeField] private InterstitialAdHandler interstitialAdHandler;

    public static AdsManager Instance => instance;
    private static AdsManager instance;

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

            interstitialAdHandler.Load();
        });
    }
    public void ShowAd(AdType type)
    {
        switch (type)
        {
            case AdType.Interstitial: interstitialAdHandler.Show(); break;
        }
    }
}
