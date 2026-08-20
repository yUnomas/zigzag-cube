using UnityEngine;

public class Bridge : StageObjectBase
{
    [SerializeField] private MeshRenderer meshRenderer;
    private Material material;

    private void Awake()
    {
        material = meshRenderer.material;
    }

    public override void Set(int startLaneIndex, float width)
    {
        base.Set(startLaneIndex, width);
        material.mainTextureScale = new Vector3(width / 2f, 1);
    }
}
