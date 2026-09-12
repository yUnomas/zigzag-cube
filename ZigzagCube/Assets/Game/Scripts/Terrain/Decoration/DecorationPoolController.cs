using UnityEngine;

public class DecorationPoolController : MonoBehaviour
{
    [SerializeField] private StageObjectPool grassPool;
    [SerializeField] private StageObjectPool flowerPool;

    public StageObjectBase Get(DecorationType type)
    {
        switch (type)
        {
            case DecorationType.Grass: return grassPool.Get();
            case DecorationType.Flower: return flowerPool.Get();
            default: return null;
        }
    }
    public void Release(DecorationType type, StageObjectBase target)
    {
        if (type == DecorationType.None || target == null) return;

        switch (type)
        {
            case DecorationType.Grass: grassPool.Release(target); break;
            case DecorationType.Flower: flowerPool.Release(target); break;
        }
    }
}
