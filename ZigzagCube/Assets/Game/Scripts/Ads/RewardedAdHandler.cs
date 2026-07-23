using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class RewardedAdHandler : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private Action onRewarded;

#if UNITY_IOS
    private const string RewardedAdUnitID =
        "ca-app-pub-3940256099942544/1712485313";
#elif UNITY_ANDROID
    private const string RewardedAdUnitID =
        "ca-app-pub-3940256099942544/5224354917";
#else
    private const string RewardedAdUnitID = "unused";
#endif

    public void Load()
    {
        Destroy();

#if UNITY_IOS || UNITY_ANDROID
        // リワード広告のリクエスト送信
        var request = new AdRequest();
        RewardedAd.Load(RewardedAdUnitID, request, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null)
            {
                Debug.LogWarning($"リワード広告の読み込み失敗\n{error}");
                return;
            }
            if (ad == null)
            {
                Debug.LogWarning("インタースティシャル広告がnull");
                return;
            }

            Debug.Log("インタースティシャル広告の読み込み完了");
            rewardedAd = ad;
            RegisterAdEvents();
        });
#endif
    }
    private void Destroy()
    {
        if (rewardedAd == null) return;

        rewardedAd.Destroy();
        rewardedAd = null;
    }
    private void RegisterAdEvents()
    {
        rewardedAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.LogWarning($"リワード広告の表示開始");
            AudioListener.pause = true;
        };
        rewardedAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.LogWarning($"リワード広告の表示終了");
            AudioListener.pause = false;
            Load();
        };
        rewardedAd.OnAdFullScreenContentFailed += error =>
        {
            Debug.LogWarning($"リワード広告の表示失敗\n{error}");
            AudioListener.pause = false;
            Load();
        };
    }
    public void Show()
    {
        rewardedAd.Show((Reward reward) =>
        {
            onRewarded?.Invoke();
        });
    }
    public void SetReward(Action action)
    {
        onRewarded = action;
    }
}