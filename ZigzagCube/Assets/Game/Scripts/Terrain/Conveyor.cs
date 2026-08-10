using UnityEngine;

public class Conveyor : MonoBehaviour
{
    [SerializeField, Tooltip("コンベヤーの流れる速度")]
    private float flowSpeed;
    [Header("=====")]
    [SerializeField] private MeshRenderer topMeshRenderer;
    [SerializeField] private MeshRenderer sideMeshRenderer;

    private Material topMaterial;
    private Material sideMaterial;
    [SerializeField] private int flowDirection;

    private void Awake()
    {
        topMaterial = topMeshRenderer.material;
        sideMaterial = sideMeshRenderer.material;
    }
    private void Update()
    {
        // コンベヤーの流れるアニメーション
        Vector2 offset = topMaterial.mainTextureOffset;
        offset.x -= flowSpeed * Time.deltaTime;
        topMaterial.mainTextureOffset = offset;
        sideMaterial.mainTextureOffset = topMaterial.mainTextureOffset;
    }

    public void Clear()
    {
        gameObject.SetActive(false);
    }
    public void Set(int width, int direction)
    {
        gameObject.SetActive(true);
        transform.localScale = new Vector3(width, transform.localScale.y, transform.localScale.z);
        topMaterial.mainTextureScale = new Vector3(width / 2f * direction, 1);
        sideMaterial.mainTextureScale = new Vector3(0.5f * direction, 1);
        flowDirection = direction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().AddSpeed(flowSpeed * flowDirection);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().RemoveSpeed(flowSpeed * flowDirection);
        }
    }
}
