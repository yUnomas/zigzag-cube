using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Tooltip("飛距離")]
    private float distance = 15f;
    [SerializeField]
    private float speed = 5f;

    private Vector3 startPos;

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        if (startPos.z - transform.position.z >= distance) Clear();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerDeath>().Die(collision.GetContact(0).point);
        }
        Clear();
    }

    private void Clear()
    {
        gameObject.SetActive(false);
        transform.position = startPos;
    }
    public void Set(Vector3 startPos)
    {
        gameObject.SetActive(true);
        this.startPos = startPos;
    }
}
