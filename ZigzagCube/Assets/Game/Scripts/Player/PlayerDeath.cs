using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField, Tooltip("ヒット対象のタグ一覧")]
    private List<string> hitTags = new List<string>();
    [SerializeField, Tooltip("障害物衝突時のエフェクト")]
    private GameObject obstacleHitEffect;

    private void Death(ContactPoint contact)
    {
        GameObject obj = Instantiate(obstacleHitEffect, contact.point, Quaternion.identity);
        obj.GetComponent<EffectBase>().PlayOnce();

        Debug.Log("ゲームオーバー");
        GetComponent<BoxCollider>().enabled = false;        // 当たり判定を消す
        GetComponent<Rigidbody>().useGravity = false;       // 重力の働きを停止
        GetComponent<PlayerController>().enabled = false;   // プレイヤーの機能停止
        transform.GetChild(0).gameObject.SetActive(false);  // モデルを非表示
        GameplayManager.Instance.GameOver();
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (string tag in hitTags)
        {
            if (collision.gameObject.tag == tag)
            {
                Death(collision.contacts[0]);
                break;
            }
        }
    }
/*    private void OnTriggerEnter(Collider other)
    {
        foreach (string tag in hitTags)
        {
            if (other.gameObject.tag == tag)
            {
                Death();
                break;
            }
        }
    }*/
}
