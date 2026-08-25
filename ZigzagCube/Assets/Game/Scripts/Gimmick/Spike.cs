using UnityEngine;

public class Spike : StageObjectBase
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform leftEndPoint;
    [SerializeField] private Transform rightEndPoint;
    [Header("=====")]
    [SerializeField] private Transform spike;
    [SerializeField] private GameObject lane;

    /// <summary>
    /// 移動方向    </summary>
    Vector3 direction = Vector3.right;
    /// <summary>
    /// 移動の有無    </summary>
    private bool isMoving;

    private void Update()
    {
        if(isMoving)
        {
            spike.position += speed * direction * Time.deltaTime;
            TryTurn();
        }
    }

    /// <summary>
    /// 折り返せるなら折り返す    </summary>
    private void TryTurn()
    {
        // レーンの左端にスパイクが到達したら右方向へ折り返し
        if (leftEndPoint.position.x >= spike.position.x)
        {
            spike.position = leftEndPoint.position;
            direction = Vector3.right;
        }
        // レーンの右端にスパイクが到達したら左方向へ折り返し
        else if (rightEndPoint.position.x <= spike.position.x)
        {
            spike.position = rightEndPoint.position;
            direction = Vector3.left;
        }
    }
    private void SetSpike(int laneIndex, int width)
    {
        int x = laneIndex - (width - 1) / 2;
        // 表示
        gameObject.SetActive(true);
        // Transform設定
        Vector3 pos = spike.transform.localPosition;
        pos.x = x;
        spike.transform.localPosition = pos;
    }
    private void SetLane()
    {
        lane.SetActive(true);
    }

    public override void Clear()
    {
        base.Clear();
        if (lane.activeSelf) lane.SetActive(false);
    }
    /// <summary>
    /// スパイクの設定    </summary>
    public void Set(int laneIndex, int width, int direction, bool isMoving = false)
    {
        SetSpike(laneIndex, width); // スパイクの配置
        this.direction = direction == 1 ? Vector3.right : Vector3.left;
        this.isMoving = isMoving;
    }
    /// <summary>
    /// レーン上を移動するスパイクの設定    </summary>
    public void SetWithLane(int startLaneIndex, int width, int direction)
    {
        Set(startLaneIndex, width, direction, true);
        SetLane();
    }
}
