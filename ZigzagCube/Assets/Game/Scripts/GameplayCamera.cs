using UnityEngine;

public class GameplayCamera : MonoBehaviour
{
    [SerializeField, Tooltip("移動速度。値が高くなれば、スムーズ移動が速くなる")]
    private float speed = 1f;
    [SerializeField, Tooltip("プレイヤーからのオフセット値")]
    private Vector3 offset;
    [Header("=====")]
    [SerializeField] private GameObject player;

    private void FixedUpdate()
    {
        Vector3 current = transform.position;
        Vector3 target = player.transform.position + offset;
        transform.position = Vector3.Lerp(current, target, speed * Time.deltaTime);
    }
}