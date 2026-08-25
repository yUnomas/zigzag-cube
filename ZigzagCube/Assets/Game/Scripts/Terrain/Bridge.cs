using UnityEngine;

public class Bridge : StageObjectBase
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform leftEndPoint;
    [SerializeField] private Transform rightEndPoint;
    [SerializeField] private float endPointOffset;
    [Header("=====")]
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Transform bridge;
    [SerializeField] private GameObject lane;
    private Material material;

    /// <summary>
    /// 移動方向    </summary>
    Vector3 direction = Vector3.right;
    /// <summary>
    /// 移動の有無    </summary>
    private bool isMoving;

    private void Awake()
    {
        material = meshRenderer.material;
    }
    private void Update()
    {
        if(isMoving)
        {
            bridge.position += speed * direction * Time.deltaTime;
            TryTurn();
        }
    }

    /// <summary>
    /// 折り返せるなら折り返す    </summary>
    private void TryTurn()
    {
        // レーンの左端にスパイクが到達したら右方向へ折り返し
        if (leftEndPoint.position.x >= bridge.position.x - bridge.localScale.x / 2 + endPointOffset)
        {
            Vector3 pos = bridge.position;
            pos.x = leftEndPoint.position.x + bridge.localScale.x / 2 - endPointOffset;
            bridge.position = pos;

            direction = Vector3.right;
        }
        // レーンの右端にスパイクが到達したら左方向へ折り返し
        else if (rightEndPoint.position.x <= bridge.position.x + bridge.localScale.x / 2 - endPointOffset)
        {
            Vector3 pos = bridge.position;
            pos.x = rightEndPoint.position.x - bridge.localScale.x / 2 + endPointOffset;
            bridge.position = pos;

            direction = Vector3.left;
        }
    }
    private void SetBridge(int startLaneIndex, float width)
    {
        float x = startLaneIndex + (width - 1) / 2;
        // 表示
        gameObject.SetActive(true);
        material.mainTextureScale = new Vector3(width / 2f, 1);
        // Transform設定
        Vector3 pos = bridge.localPosition;
        pos.x = x;
        bridge.localPosition = pos;
        bridge.localScale = new Vector3(width, 1, 1);
    }
    private void SetLane()
    {
        lane.SetActive(true);
    }

    public override void Clear()
    {
        base.Clear();
        if(lane.activeSelf) lane.SetActive(false);
    }
    /// <summary>
    /// 移動する橋の設定    </summary>
    public void Set(int startLaneIndex, float width, int direction, bool isMoving)
    {
        SetBridge(startLaneIndex, width);
        this.direction = direction == 1 ? Vector3.right : Vector3.left;
        this.isMoving = isMoving;
    }
    /// <summary>
    /// 移動する橋＋レーンの設定    </summary>
    public void SetWithLane(int startLaneIndex, float width, int direction)
    {
        Set(startLaneIndex, width, direction, true);
        SetLane();
    }
}