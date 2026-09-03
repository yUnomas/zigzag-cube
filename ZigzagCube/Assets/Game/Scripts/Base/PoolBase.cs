using UnityEngine;
using UnityEngine.Pool;

public abstract class PoolBase<T> : MonoBehaviour where T : Component
{
    [SerializeField, Tooltip("初回のプール数")]
    protected int defaultCapacity = 10;
    [SerializeField, Tooltip("最大プール数")]
    private int maxSize = 20;
    [SerializeField, Tooltip("起動時に defaultCapacity 分を事前生成しておくか")]
    private bool prewarmOnAwake = true;
    [Header("=====")]
    [SerializeField] protected T prefab;

    private ObjectPool<T> pool;

    private void Awake()
    {
        pool = new ObjectPool<T>(
                createFunc: CreateInstance,
                actionOnGet: OnGetInstance,
                actionOnRelease: OnReleaseInstance,
                actionOnDestroy: OnDestroyInstance,
                defaultCapacity: defaultCapacity,
                maxSize: maxSize
            );

        if (prewarmOnAwake) Prewarm();
    }

    /// <summary>
    /// defaultCapacity の数だけ事前にプールに蓄積   </summary>
    private void Prewarm()
    {
        var tempArray = new T[defaultCapacity];

        // 指定数だけ生成して取り出す
        for (int i = 0; i < defaultCapacity; i++)
        {
            tempArray[i] = pool.Get();
            // 生成したインスタンスは返却されずに取り出し続ける
            // これにより、指定数だけ正常に生成される
        }
        // 全てプールに返却
        for (int i = 0; i < defaultCapacity; i++)
        {
            pool.Release(tempArray[i]);
        }
    }

    protected virtual T CreateInstance()
    {
        return Instantiate(prefab, transform);
    }
    protected virtual void OnGetInstance(T target)
    {
        target.gameObject.SetActive(true);
    }
    protected virtual void OnReleaseInstance(T target)
    {
        target.gameObject.SetActive(false);
        target.transform.parent = transform;
        target.transform.position = transform.position;
    }
    protected virtual void OnDestroyInstance(T target)
    {
        Destroy(target.gameObject);
    }

    public virtual T Get() { return pool.Get(); }
    public virtual void Release(T target) { if (target != null) pool.Release(target); }
}
