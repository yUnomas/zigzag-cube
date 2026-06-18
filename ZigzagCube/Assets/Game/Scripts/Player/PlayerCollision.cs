using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField, Tooltip("ヒット対象のタグ一覧")]
    private List<string> hitTags = new List<string>();

    private void OnCollisionEnter(Collision collision)
    {
        foreach(string tag in hitTags)
        {
            if(collision.gameObject.tag == tag)
            {
                HitCollision();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        foreach(string tag in hitTags)
        {
            if(other.gameObject.tag == tag)
            {
                HitTrigger();
            }
        }
    }

    protected void HitCollision()
    {
        Debug.Log("ゲームオーバー");
        GetComponent<PlayerController>().enabled = false;   // プレイヤーの機能停止
        GameplayManager.Instance.GameOver();
    }
    protected void HitTrigger()
    {

    }
}
