using UnityEngine;

public class SpikeLane : StageObjectBase
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform spike;
    [SerializeField] private Transform leftEndPoint;
    [SerializeField] private Transform rightEndPoint;

    Vector3 direction = Vector3.right;

    private void Update()
    {
        spike.position += speed * direction * Time.deltaTime;
        TryTurn();
    }

    private void SetLane(int width)
    {
        float x = (width - 1) / 2;
        // 表示
        gameObject.SetActive(true);
/*        // Transform設定
        Vector3 pos = transform.localPosition;
        pos.x = x;
        transform.localPosition = pos;*/
    }
    private void SetSpike(int laneIndex, int width)
    {
        int x = laneIndex - (width - 1) / 2;
        // Transform設定
        Vector3 pos = spike.transform.localPosition;
        pos.x = x;
        spike.transform.localPosition = pos;
    }
    /// <summary>
    /// 折り返せるなら折り返す    </summary>
    private void TryTurn()
    {
        // レーンの左端にスパイクが到達したら右方向へ折り返し
        if(leftEndPoint.position.x >= spike.position.x)
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


    public void Set(int laneIndex, int width, int direction)
    {
        SetLane(width);             // レーンの配置
        SetSpike(laneIndex, width); // スパイクの配置
        this.direction = direction == 1 ? Vector3.right : Vector3.left;
    }
}