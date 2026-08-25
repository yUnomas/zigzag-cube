using UnityEngine;

public class Cannon : StageObjectBase
{
    [SerializeField, Tooltip("発射間隔")]
    private float duration = 120f;
    [Header("=====")]
    [SerializeField] private Bullet[] bullets;

    private float elapsedTime;

    private void Update()
    {
        if (elapsedTime >= duration)
        {
            SetBullet(transform.position);
            elapsedTime = 0f;
        }
        else elapsedTime += Time.deltaTime;
    }

    private void SetBullet(Vector3 startPos)
    {
        foreach(Bullet bullet in bullets)
        {
            if(!bullet.gameObject.activeSelf)
            {
                bullet.Set(startPos);
                break;
            }
        }
    }
    public override void Clear()
    {
        base.Clear();
        elapsedTime = 0f;
    }
}