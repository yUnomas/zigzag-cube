using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : ControllerBase
{
    protected override InputData CreateInputData()
    {
        InputData data = new InputData();
        if(InputHelper.IsPointerDown() && !InputHelper.IsPointerOverUI())
        {
            data.isTouch = true;
        }
        return data;
    }
}
