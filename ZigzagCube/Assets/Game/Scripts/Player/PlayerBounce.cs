using UnityEngine;

public class PlayerBounce : ModuleBase<PlayerController>
{
    [SerializeField] private PlayerMovement playerMovement;
    private const string wallTag = "Wall";

    private void OnCollisionEnter(Collision collision)
    {
        // 横から壁に当たった場合、跳ね返る
        if (collision.gameObject.CompareTag(wallTag))
        {
            Vector3 normal = collision.contacts[0].normal;
            if (Mathf.Abs(normal.x) > 0.9f)
            {
                Bounce();
            }
        }
    }

    /// <summary>
    /// 跳ね返り処理    </summary>
    private void Bounce()
    {
        playerMovement.TriggerDirection();
        AudioManager.Instance.PlaySE("WallHit", false);
    }
}
