using GoogleMobileAds.Api;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    [SerializeField, Tooltip("広告の表示間隔")]
    private int displayInterval = 3;
    [SerializeField] private InterstitialAdHandler interstitialAdHandler;

    public static AdsManager Instance => instance;
    private static AdsManager instance;

    /// <summary>
    /// プレイ回数    </summary>
    private int sessionPlayCount;

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
    /// <summary>
    /// 広告表示    </summary>
    public void ShowAd(AdType type)
    {
        switch (type)
        {
            case AdType.Interstitial: interstitialAdHandler.Show(); break;
        }
    }
    /// <summary>
    /// 表示可能な場合のみ広告を表示    </summary>
    public void TryShowAd(AdType type)
    {
        switch (type)
        {
            case AdType.Interstitial:
                {
                    if(IsInterstitialTiming()) interstitialAdHandler.Show();
                }
                break;
        }
    }
    /// <summary>
    /// インタースティシャル広告の表示タイミングか判定    </summary>
    /// <returns>
    /// セッション中の1プレイ目、または一定の表示間隔に達した場合は true </returns>
    public bool IsInterstitialTiming()
    {
        return sessionPlayCount == 1 || (sessionPlayCount - 1) % displayInterval == 0;
    }

    /// <summary>
    /// ゲームプレイ終了時のイベント    </summary>
    public void OnGameplayEnded()
    {
        sessionPlayCount++;
    }
}