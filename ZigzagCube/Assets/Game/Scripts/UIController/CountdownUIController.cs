using System;
using TMPro;
using UnityEngine;

public class CountdownUIController : UIControllerBase
{
    [SerializeField] private TextMeshProUGUI countdownTMP;

    private async Awaitable CountdownAsync(int seconds, Action onCompleted)
    {
        // 渡された秒数だけカウントダウン
        int timer = seconds;
        while (timer > 0)
        {
            countdownTMP.SetText($"{timer}");
            AudioManager.Instance.PlaySE("UIButtonClose");

            await Awaitable.WaitForSecondsAsync(1f);
            timer--;
        }
        Hide(); // カウントダウン終了と共にUI非表示
        onCompleted?.Invoke();
    }

    public void Countdown(int seconds, Action onCompleted)
    {
        Show(); // カウントダウン終了と共にUI表示
        _ = CountdownAsync(seconds, onCompleted);
    }
}
