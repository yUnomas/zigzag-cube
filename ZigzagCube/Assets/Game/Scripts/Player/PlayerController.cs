using UnityEngine;

public class PlayerController : ControllerBase
{
    protected override InputData CreateInputData()
    {
        InputData data = new InputData();
        //** タッチ入力
        if (Input.touchCount > 0)
        {
            data.isTouch = Input.GetTouch(0).phase == TouchPhase.Began;
        }
#if UNITY_EDITOR
        // Editor上では、左クリックをタッチ入力とする
        data.isTouch = Input.GetMouseButtonDown(0);
#endif

        return data;
    }
}
