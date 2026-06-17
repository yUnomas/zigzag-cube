using UnityEngine;

[CreateAssetMenu(fileName = "AudioDefinition", menuName = "ScriptableObjects/AudioDefinition")]
public class AudioDefinition : ScriptableObject
{
    [Tooltip("音源")]
    public AudioClip clip;
    [Range(0f, 1f), Tooltip("音量")]
    public float volume = 1f;
    [Tooltip("ループの有無")]
    public bool isLoop;
}