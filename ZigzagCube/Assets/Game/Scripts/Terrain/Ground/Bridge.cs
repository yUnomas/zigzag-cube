using UnityEngine;

public class Bridge : StageObjectBase
{
    [SerializeField] private MeshRenderer meshRenderer;
    private Material material;

    private void Awake()
    {
        material = meshRenderer.material;
    }
    
    public override void Set(Transform cell, GroundData data)
    {
        material.mainTextureScale = new Vector3(data.width / 2f, data.length);
        base.Set(cell, data);
    }
}