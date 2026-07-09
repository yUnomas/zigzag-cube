using UnityEngine;
using UnityEngine.EventSystems;

public static class InputHelper
{
    /// <summary>
    /// ポインターの押下状態か判定    </summary>
    public static bool IsPointerDown()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return Input.GetMouseButtonDown(0);
#else
    return Input.touchCount > 0 &&
           Input.GetTouch(0).phase == TouchPhase.Began;
#endif
    }
    /// <summary>
    /// ポインターがUI上か判定    </summary>
    /// <returns></returns>
    public static bool IsPointerOverUI()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return EventSystem.current != null &&
               EventSystem.current.IsPointerOverGameObject();
#else
        if (Input.touchCount == 0)
            return false;

        return EventSystem.current != null &&
               EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
#endif
    }
}