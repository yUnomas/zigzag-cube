using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private AudioSource bgmSource;
    [SerializeField]
    private List<AudioSource> d2Sources;
    [SerializeField]
    private List<AudioSource> d3Sources;
    [SerializeField] private List<AudioData> audioDataList;

    public static AudioManager Instance => instance;
    private static AudioManager instance;

    /// <summary>
    /// 高速検索用の音声データ辞書    </summary>
    private Dictionary<string, AudioData> audioDataDict = new();

    private void Awake()
    {
        // インスタンス化
        if(instance == null)
        {
            instance = this;
            InitializeDictionary();
        }
    }

    /// <summary>
    /// 検索用の辞書を初期化    </summary>
    private void InitializeDictionary()
    {
        // リストから辞書へ変換
        foreach (var data in audioDataList)
        {
            if (data != null && !string.IsNullOrEmpty(data.id))
            {
                audioDataDict.Add(data.id, data);
            }
        }
    }
    /// <summary>
    /// 音声データの取得(有無の確認込み)    </summary>
    private AudioData TryGetData(string id)
    {
        if (audioDataDict.ContainsKey(id))
        {
            return audioDataDict[id];
        }
        else
        {
            Debug.LogWarning($"音声ID: {id} が見つかりません");
            return null;
        }
    }
    /// <summary>
    /// 使用可能な2Dソースを取得    </summary>
    /// <param name="isAllowDuplicate">
    /// 重複を許可するかどうか </param>
    private AudioSource TryGetAvailable2DSource(AudioClip clip, bool isAllowDuplicate)
    {
        AudioSource availableSource = null;
        foreach (AudioSource source in d2Sources)
        {
            // 重複禁止時に音声被りが発生したら処理終了
            if (!isAllowDuplicate && source.clip == clip && source.isPlaying)
            {
                Debug.LogWarning($"音声: {clip} の重複再生は許可されていません");
                return null;
            }
            // 再生していないソースを保持
            if (!source.isPlaying) availableSource = source;
        }
        // 利用可能なソースがあれば、ソースを返す
        if (availableSource) return availableSource;
        else
        {
            Debug.LogWarning($"再生可能な3D用SEソースが見つかりません");
            return null;
        }
    }
    /// <summary>
    /// 使用可能な3Dソースを取得    </summary>
    /// <param name="isAllowDuplicate">
    /// 重複を許可するかどうか </param>
    private AudioSource TryGetAvailable3DSource(AudioClip clip, bool isAllowDuplicate)
    {
        AudioSource availableSource = null;
        foreach (AudioSource source in d3Sources)
        {
            // 重複禁止時に音声被りが発生したら処理終了
            if (isAllowDuplicate && source.clip == clip && source.isPlaying)
            {
                Debug.LogWarning($"音声: {clip} の重複再生は許可されていません");
                return null;
            }
            // 再生していないソースを保持
            if (!source.isPlaying) availableSource = source;
        }
        // 利用可能なソースがあれば、ソースを返す
        if (availableSource) return availableSource;
        else
        {
            Debug.LogWarning($"再生可能な3D用SEソースが見つかりません");
            return null;
        }
    }

    /// <summary>
    /// BGMを再生    </summary>
    /// <param name="id">
    /// 再生する音声ID    </param>
    public void PlayBGM(string id)
    {
        // 音声データの取得
        AudioData data = TryGetData(id);
        if (data == null) return;

        // ソースに各種情報を設定して再生
        bgmSource.clip = data.clip;
        bgmSource.volume = data.volume;
        bgmSource.loop = data.isLoop;
        bgmSource.Play();
    }
    /// <summary>
    /// BGMを停止    </summary>
    public void StopBGM(){bgmSource.Stop(); bgmSource.clip = null; }
    /// <summary>
    /// SEを再生(2Dがメイン)    </summary>
    /// <param name="id">
    /// 再生する音声ID    </param>
    /// <param name="isAllowDuplicate">
    /// 重複を許可するかどうか </param>
    public void PlaySE(string id, bool isAllowDuplicate = true)
    {
        // 音声データの取得
        AudioData data = TryGetData(id);
        if(data == null) return;
        // 再生可能な音声ソースの取得
        AudioSource source = TryGetAvailable2DSource(data.clip, isAllowDuplicate);
        if (source == null) return;

        // ソースに各種情報を設定して再生
        source.clip = data.clip;
        source.outputAudioMixerGroup = data.mixerGroup;
        source.volume = data.volume;
        source.loop = data.isLoop;
        source.Play();
    }
    /// <summary>
    /// SEを再生(3Dがメイン)    </summary>
    /// <param name="id">
    /// 再生する音声ID    </param>
    /// <param name="position">
    /// 再生する位置    </param>
    /// <param name="isAllowDuplicate">
    /// 重複を許可するかどうか </param>
    public void PlaySE(string id, Vector3 position, bool isAllowDuplicate = true)
    {
        // 音声データの取得
        AudioData data = TryGetData(id);
        if (data == null) return;
        // 再生可能な音声ソースの取得
        AudioSource source = TryGetAvailable2DSource(data.clip, isAllowDuplicate);
        if (source == null) return;

        // ソースに各種情報を設定して再生
        source.gameObject.transform.position = position;
        source.clip = data.clip;
        source.outputAudioMixerGroup = data.mixerGroup;
        source.volume = data.volume;
        source.loop = data.isLoop;
        source.Play();
    }

    /// <summary>
    /// 音量ボリュームを設定    </summary>
    /// <param name="type">
    /// 設定する音声タイプ    </param>
    /// <param name="volume">
    /// 設定するボリューム値    </param>
    public void SetVolume(AudioType type, float volume)
    {
        // 渡されたボリューム値を0～1の値に丸める
        volume = Mathf.Clamp01(volume);
        // デシベル変換後、AudioMixerにセット
        float dB = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20f;
        audioMixer.SetFloat(type.ToString() + "Volume", dB);
    }

    /// <summary>
    /// 設定の反映    </summary>
    public void ApplySettings(SettingsData settingsData)
    {
        SetVolume(AudioType.Master, settingsData.masterVolume);
        SetVolume(AudioType.BGM, settingsData.bgmVolume);
        SetVolume(AudioType.SE, settingsData.seVolume);
        SetVolume(AudioType.SFX, settingsData.sfxVolume);
    }
}
