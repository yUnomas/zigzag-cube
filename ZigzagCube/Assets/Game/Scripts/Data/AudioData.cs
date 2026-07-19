using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioData", menuName = "AudioData")]
public class AudioData : ScriptableObject
{
    [Tooltip("検索ID")]
    public string id;
    [Tooltip("音源")]
    public AudioClip clip;
    [Tooltip("再生ミキサー")]
    public AudioMixerGroup mixerGroup;
    [Range(0f, 1f), Tooltip("音量")]
    public float volume = 1f;
    [Tooltip("ループの有無")]
    public bool isLoop;
}