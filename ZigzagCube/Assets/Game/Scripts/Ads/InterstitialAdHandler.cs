using GoogleMobileAds.Api;
using UnityEngine;

public class InterstitialAdHandler : MonoBehaviour
{
    private InterstitialAd interstitialAd;

#if UNITY_IOS
    private const string InterstitialAdUnitID =
        "ca-app-pub-3940256099942544/4411468910";
#elif UNITY_ANDROID
    private const string InterstitialAdUnitID =
        "ca-app-pub-3940256099942544/1033173712";
#else
    private const string InterstitialAdUnitID = "unused";
#endif

    public void Load()
    {
        Destroy();

#if UNITY_IOS || UNITY_ANDROID
        // インタースティシャル広告のリクエスト送信
        var request = new AdRequest();
        InterstitialAd.Load(InterstitialAdUnitID, request,  (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null)
            {
                Debug.LogWarning($"インタースティシャル広告の読み込み失敗\n{error}");
                return;
            }
            if (ad == null)
            {
                Debug.LogWarning("インタースティシャル広告がnull");
                return;
            }

            Debug.Log("インタースティシャル広告の読み込み完了");
            interstitialAd = ad;
            RegisterAdEvents();
        });
#endif
    }
    private void Destroy()
    {
        if (interstitialAd == null) return;

        interstitialAd.Destroy();
        interstitialAd = null;
    }
    private void RegisterAdEvents()
    {
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.LogWarning($"インタースティシャル広告の表示開始");
            AudioListener.pause = true;
        };
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.LogWarning($"インタースティシャル広告の表示終了");
            AudioListener.pause = false;
            Load();
        };
        interstitialAd.OnAdFullScreenContentFailed += error =>
        {
            Debug.LogWarning($"インタースティシャル広告の表示失敗\n{error}");
            AudioListener.pause = false;
            Load();
        };
    }

    public void Show()
    {
        if (interstitialAd == null || !interstitialAd.CanShowAd())
        {
            Debug.Log("インタースティシャル広告の表示不可");
            return;
        }

        interstitialAd.Show();
    }
}
