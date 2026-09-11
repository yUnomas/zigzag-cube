using UnityEngine;

public class Conveyor : StageObjectBase
{
    [SerializeField, Tooltip("コンベヤーの流れる速度")]
    private float flowSpeed;
    [Header("=====")]
    [SerializeField] private MeshRenderer topMeshRenderer;
    [SerializeField] private MeshRenderer sideMeshRenderer;

    private Material topMaterial;
    private Material sideMaterial;
    /// <summary>
    /// コンベヤーの流れる方向     </summary>
    private int flowDirection;

    private void Awake()
    {
        topMaterial = topMeshRenderer.material;
        sideMaterial = sideMeshRenderer.material;
    }
    private void Update()
    {
        // コンベヤーが流れるアニメーション
        Vector2 offset = topMaterial.mainTextureOffset;
        offset.x -= flowSpeed * Time.deltaTime;
        topMaterial.mainTextureOffset = offset;
        sideMaterial.mainTextureOffset = topMaterial.mainTextureOffset;
    }

    public override void Set(Transform cell, GroundData data, DifficultyParameterEntry param)
    {
        flowDirection = data.direction;
        flowSpeed = param.conveyorSpeed;
        topMaterial.mainTextureScale = new Vector3(data.width / 2f * data.direction, data.length);
        sideMaterial.mainTextureScale = new Vector3(0.5f * data.direction, data.length);
        base.Set(cell, data, param);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMovement>(out var playerMovement))
        {
            playerMovement.AddExternalSpeed(flowSpeed * flowDirection);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMovement>(out var playerMovement))
        {
            playerMovement.RemoveExternalSpeed(flowSpeed * flowDirection);
        }
    }
}
