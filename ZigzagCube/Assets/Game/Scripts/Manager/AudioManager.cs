using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioType
{
    Master,
    BGM,
    SE,
}
public class AudioManager : MonoBehaviour
{
    [Header("設定不可")]
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private AudioSource bgmSource;
    [SerializeField]
    private List<AudioSource> d2Sources;
    [SerializeField]
    private List<AudioSource> d3Sources;

    /// <summary>
    /// インスタンス    </summary>
    private static AudioManager instance;
    public static AudioManager Instance => instance;

    private void Awake()
    {
        // インスタンス化
        if(instance == null) instance = this;
    }

    /// <summary>
    /// 利用可能な2Dソースを取得    </summary>
    /// <param name="def">
    /// 再生する音声データ    </param>
    /// <param name="isAllowDuplicate">
    /// 重複を許可するかどうか </param>
    private AudioSource GetAvailable2DSource(AudioDefinition def, bool isAllowDuplicate)
    {
        foreach(AudioSource source in d2Sources)
        {
            if(!source.isPlaying) return source;
            else if (!isAllowDuplicate)
            {
                if(source.clip == def.clip) return source;
            }
        }
        return null;
    }
    /// <summary>
    /// 利用可能な3Dソースを取得    </summary>
    /// <param name="def">
    /// 再生する音声データ    </param>
    /// <param name="isAllowDuplicate">
    /// 重複を許可するかどうか </param>
    private AudioSource GetAvailable3DSource(AudioDefinition def, bool isAllowDuplicate)
    {
        foreach (AudioSource source in d3Sources)
        {
            if (!source.isPlaying) return source;
            else if (!isAllowDuplicate)
            {
                if (source.clip == def.clip) return source;
            }
        }
        return null;
    }

    /// <summary>
    /// BGMを再生    </summary>
    /// <param name="def">
    /// 再生する音声データ    </param>
    public void PlayBGM(AudioDefinition def)
    {
        if(bgmSource.clip != def.clip)
        {
            bgmSource.clip = def.clip;
            bgmSource.volume = def.volume;
            bgmSource.loop = def.isLoop;
            bgmSource.Play();
        }
    }
    /// <summary>
    /// BGMを停止    </summary>
    public void StopBGM(){bgmSource.Stop(); bgmSource.clip = null; }
    /// <summary>
    /// SEを再生(2Dがメイン)    </summary>
    /// <param name="data">
    /// 再生する音声データ    </param>
    /// <param name="isAllowDuplicate">
    /// 重複を許可するかどうか </param>
    public void PlaySE(AudioDefinition def, bool isAllowDuplicate = true)
    {
        // 再生可能であれば、再生
        AudioSource source = GetAvailable2DSource(def, isAllowDuplicate);
        if(source != null)
        {
            source.clip = def.clip;
            source.volume = def.volume;
            source.loop = def.isLoop;
            source.Play();
        }
        else Debug.Log("2D用SEソースが足りていません。");
    }
    /// <summary>
    /// SEを再生(3Dがメイン)    </summary>
    /// <param name="def">
    /// 再生する音声データ    </param>
    /// <param name="position">
    /// 再生する位置    </param>
    /// <param name="isAllowDuplicate">
    /// 重複を許可するかどうか </param>
    public void PlaySE(AudioDefinition def, Vector3 position, bool isAllowDuplicate = true)
    {
        // 再生可能であれば、再生
        AudioSource source = GetAvailable3DSource(def, isAllowDuplicate);
        if (source != null)
        {
            source.gameObject.transform.position = position;
            source.clip = def.clip;
            source.volume = def.volume;
            source.loop = def.isLoop;
            source.Play();
        }
        else Debug.Log("3D用SEソースが足りていません。");
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
}
