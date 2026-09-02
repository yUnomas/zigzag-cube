using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    [Header("設定可")]
    [SerializeField]
    [Tooltip("デフォルトのフェード期間")]
    private float defaultFadeDuration = 1f;
    [Header("設定不可")]
    [SerializeField]
    private Image fadeOverlay;

    private static FadeManager instance;
    public static FadeManager Instance => instance;
    /// <summary>
    /// フェード処理の実行状態    </summary>
    public bool isFading { get; private set; }
    /// <summary>
    /// フェードアウト状態    </summary>
    public bool isFadeOut { get; private set; }

    private void Awake()
    {
        // インスタンス化
        if (instance == null)   instance = this;
    }

    /// <summary>
    /// フェード実行    </summary>
    /// <param name="endAlpha">
    /// 最終α値    </param>
    /// <param name="onComplete">
    /// 完了時に発火する内容    </param>
    private IEnumerator FadeRunning(float endAlpha, float fadeDuration, Action onComplete = null, bool isFadeOut = true)
    {
        isFading = true;

        Color color = fadeOverlay.color;
        float timer = 0f;
        float startAlpha = color.a;
        // フェードの期間だけループ
        while(timer < fadeDuration)
        {
            // アルファ値の更新
            color.a = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);
            fadeOverlay.color = color;
            // タイム加算
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        color.a = endAlpha;
        fadeOverlay.color = color;

        // 設定されているアクション実行
        onComplete?.Invoke();
        yield return new WaitForSecondsRealtime(0.01f);

        isFading = false;
    }

    /// <summary>
    /// フェードイン(画面が現れる)    </summary>
    /// <param name="onComplete">
    /// 完了時に発火する内容  </param>
    public void FadeIn(float fadeDuration = -1f, Action onComplete = null)
    {
        if (fadeDuration < 0f) fadeDuration = defaultFadeDuration;
        if (!isFading)
        {
            isFadeOut = false;
            StartCoroutine(FadeRunning(0f, fadeDuration, onComplete));
        }
    }
    /// <summary>
    /// フェードアウト(画面が消える)    </summary>
    /// <param name="onComplete">
    /// 完了時に発火する内容  </param>
    public void FadeOut(float fadeDuration = -1f, Action onComplete = null)
    {
        if (fadeDuration < 0f) fadeDuration = defaultFadeDuration;
        if (!isFading)
        {
            isFadeOut = true;
            StartCoroutine(FadeRunning(1f, fadeDuration, onComplete));
        }
    }
    /// <summary>
    /// フェードアウトされている場合にフェードインを実行    </summary>
    public void TryFadeIn(float fadeDuration = -1f, Action onComplete = null)
    {
        if (isFadeOut) FadeIn(fadeDuration, onComplete);
    }
}