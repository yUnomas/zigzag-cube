using System.Collections.Generic;
using UnityEngine;

public class PlayerBounce : MonoBehaviour
{
    [SerializeField, Tooltip("ヒット対象のタグ一覧")]
    private List<string> hitTags = new List<string>();

    private void OnCollisionEnter(Collision collision)
    {
        // ヒット対象との衝突で跳ね返り
        foreach (string tag in hitTags)
        {
            // 横から壁に当たった場合、跳ね返る
            if (collision.gameObject.CompareTag(tag))
            {
                Vector3 normal = collision.contacts[0].normal;
                if(Mathf.Abs(normal.x) > 0.9f)
                {
                    Bounce();
                }
                break;
            }
        }
    }

    /// <summary>
    /// 跳ね返り処理    </summary>
    private void Bounce()
    {
        GetComponent<PlayerMovement>().TriggerDirection();
        AudioManager.Instance.PlaySE("WallHit", false);
    }
}
