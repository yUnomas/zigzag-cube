using UnityEngine;

public class Bridge : GroundView
{
    [SerializeField] private MeshRenderer meshRenderer;
    private Material material;

    private void Awake()
    {
        material = meshRenderer.material;
    }

    public override void View(int startLaneIndex, float width)
    {
        base.View(startLaneIndex, width);
        material.mainTextureScale = new Vector3(width / 2f, 1);
    }
}
