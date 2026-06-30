using UnityEngine;

public class EffectPlayer : MonoBehaviour
{
    [SerializeField, Tooltip("再生開始切り替え")]
    private bool playOnAwake = true;
    [SerializeField, Tooltip("終了後の削除切り替え")]
    private bool destroyOnFinished = true;
    [Header("=====")]
    [SerializeField] protected ParticleSystem effect;

    protected virtual void Start()
    {
        if(playOnAwake) Play();
        if(destroyOnFinished) Destroy(effect.main.duration);
    }
    /// <summary>
    /// 一回だけエフェクト再生    </summary>
    public virtual void PlayOnce()
    {
        effect.Play();
        Destroy(effect.main.duration);
    }
    /// <summary>
    /// エフェクト再生    </summary>
    public virtual void Play()
    {
        effect.Play();
    }
    /// <summary>
    /// エフェクト削除 </summary>
    /// <param name="t">
    /// 削除までの期間 </param>
    public virtual void Destroy(float t = 0)
    {
        Destroy(effect.gameObject, t);
    }

}
