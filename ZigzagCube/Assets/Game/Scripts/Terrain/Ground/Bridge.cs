using UnityEngine;

public class Bridge : StageObjectBase
{
    [SerializeField] private MeshRenderer meshRenderer;
    private Material material;

    private void Awake()
    {
        material = meshRenderer.material;
    }
    
    public override void Set(Transform cell, int laneIndex, int y, float width)
    {
        base.Set(cell, laneIndex, y, width);
        material.mainTextureScale = new Vector3(width / 2f, 1);
    }
}