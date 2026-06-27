using System.Collections.Generic;
using UnityEngine;

public class PlayerBounce : MonoBehaviour
{
    [SerializeField, Tooltip("ヒット対象のタグ一覧")]
    private List<string> hitTags = new List<string>();

    private void Bounce()
    {
        GetComponent<PlayerMovement>().TriggerDirection();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ヒット対象との衝突で跳ね返り
        foreach (string tag in hitTags)
        {
            if (collision.gameObject.tag == tag)
            {
                Bounce();
                break;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        foreach (string tag in hitTags)
        {
            if (other.gameObject.tag == tag)
            {
                Bounce();
                break;
            }
        }
    }
}
