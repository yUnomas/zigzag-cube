using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Apple.GameKit;
using Apple.GameKit.Leaderboards;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameCenterManager : MonoBehaviour
{
    private static GameCenterManager instance;
    public static GameCenterManager Instance => instance;

    private bool isAuthenticated;

    private const string LeaderboardID = "com.yunomas.zigzagvoxel.highscore.distance";

    private void Awake()
    {
        instance = this;

#if UNITY_IOS && !UNITY_EDITOR
        _ = Authenticate();
#else
        isAuthenticated = false;
#endif
    }

    /// <summary>
    /// GameCenterのプレイヤー認証    </summary>
    private async Awaitable Authenticate()
    {
        try
        {
            var player = await GKLocalPlayer.Authenticate();
            isAuthenticated = player.IsAuthenticated;

            Debug.Log($"GameCenter のログイン認証。\nState:{isAuthenticated}, Name:{player.DisplayName}");
        }
        catch (GameKitException e)
        {
            isAuthenticated = false;
            Debug.LogError($"GameCenter のログイン失敗。\nError:{e.Message}");
        }
    }
    /// <summary>
    /// GameCenterへのスコア送信    </summary>
    private async Awaitable SubmitScoreAsync(int score)
    {
        try
        {
            // 指定IDのリーダーボード取得
            var leaderboards =
                await GKLeaderboard.LoadLeaderboards(LeaderboardID);
            var leaderboard = leaderboards.FirstOrDefault();

            if (leaderboard == null)
            {
                Debug.LogError($"Leaderboard が見つかりません。ID:{LeaderboardID}");
                return;
            }

            // リーダーボードにスコア送信
            await leaderboard.SubmitScore(
                score,
                0,
                GKLocalPlayer.Local
            );
        }
        catch (GameKitException e)
        {
            Debug.LogError(e);
        }
    }

    public async Awaitable SubmitScore(int score)
    {
        Debug.Log("GameCenterManager.SubmitScore()を実行");
        if (!isAuthenticated) return;

        await SubmitScoreAsync(score);
    }
    public async void ShowLeaderboard()
    {
        Debug.Log("GameCenterManager.ShowLeaderboard()を実行");
        if (!isAuthenticated) return;

        var gameCenter = GKGameCenterViewController.Init(
            GKGameCenterViewControllerState.Leaderboards);
        await gameCenter.Present();
    }
}
// プレイヤーの認証状態の確認:       player.IsAuthenticated
// 現在のGameCenterプレイヤーの取得: GKLocalPlayer.Local