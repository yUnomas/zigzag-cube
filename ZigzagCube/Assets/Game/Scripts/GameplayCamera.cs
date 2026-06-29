using UnityEngine;

public class GameplayCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private GameObject player;

    private void FixedUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
