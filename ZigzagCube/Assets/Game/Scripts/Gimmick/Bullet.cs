using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Tooltip("弾速")]
    private float speed = 5f;
    [SerializeField, Tooltip("この距離以上プレイヤーの後ろに進むと削除")]
    private float clearDistance = 8f;

    private Cannon cannon;
    private PlayerController player;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>();
    }
    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        TryClearByDistance();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerDeath>().Die(collision.GetContact(0).point);
        }
        else if (collision.gameObject == cannon.gameObject) return;

        Clear();
    }

    /// <summary>
    /// プレイヤーとの距離に応じて削除    </summary>
    private void TryClearByDistance()
    {
        if (player.transform.position.z - transform.position.z >= clearDistance) Clear();
    }
    private void Clear()
    {
        gameObject.SetActive(false);
        transform.position = cannon.transform.position;
    }
    public void Set(Cannon cannon)
    {
        gameObject.SetActive(true);
        this.cannon = cannon;
    }
}
