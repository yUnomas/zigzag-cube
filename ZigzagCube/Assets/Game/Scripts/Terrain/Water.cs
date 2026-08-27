using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField, Tooltip("衝突時の水しぶきエフェクト")]
    private GameObject waterSplashEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 衝突位置が存在しない場合は衝突対象の座標を設定
            Vector3 contactPoint = collision.contactCount > 0
            ? collision.GetContact(0).point
            : collision.transform.position;

            collision.gameObject.GetComponent<PlayerDeath>().Die(DeathType.Fall, collision.GetContact(0).point);
            Instantiate(waterSplashEffect, contactPoint, Quaternion.identity);
            AudioManager.Instance.PlaySE("WaterSplash", false);
        }
    }
}
