using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField, Tooltip("障害物衝突時のエフェクト")]
    private GameObject obstacleHitEffect;
    [SerializeField, Tooltip("水衝突時のエフェクト")]
    private GameObject waterHitEffect;
    [SerializeField] private GameObject playerModel;

    private const string waterTag = "Water";
    private const string obstacleTag = "Obstacle";

    private void Death(Vector3 deathPoint, GameObject effect, string audioID)
    {
        // エフェクト・SEの再生
        Instantiate(effect, deathPoint, Quaternion.identity);
        AudioManager.Instance.PlaySE(audioID, false);
        // 死亡処理
        Debug.Log("ゲームオーバー");
        GetComponent<Rigidbody>().useGravity = false;       // 重力の働きを停止
        GetComponent<PlayerController>().enabled = false;   // プレイヤーの機能停止
        playerModel.SetActive(false);                       // モデルを非表示
        GameplayManager.Instance.GameOver();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 障害物との衝突時
        if (collision.gameObject.CompareTag(obstacleTag))
        {
            Death(collision.contacts[0].point, obstacleHitEffect, "PlayerBreak");
        }
        // 水との衝突時
        else if (collision.gameObject.CompareTag(waterTag))
        {
            Death(collision.contacts[0].point, waterHitEffect, "WaterSplash");
        }
    }
}
