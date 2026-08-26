using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Tooltip("飛距離")]
    private float distance = 15f;
    [SerializeField]
    private float speed = 5f;

    private Cannon cannon;

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        if (cannon.transform.position.z - transform.position.z >= distance) Clear();
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
