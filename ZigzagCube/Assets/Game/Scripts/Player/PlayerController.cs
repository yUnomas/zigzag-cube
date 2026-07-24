using UnityEngine;

public class PlayerController : ControllerBase
{
    [SerializeField]
    private GameObject model;

    public PlayerState State => state;
    private PlayerState state;
    
    protected override InputData CreateInputData()
    {
        InputData data = new InputData();
        if(InputHelper.IsPointerDown() && !InputHelper.IsPointerOverUI())
        {
            data.isTouch = true;
        }
        return data;
    }
    public void ChangeState(PlayerState newState)
    {
        state = newState;
        switch(newState)
        {
            case PlayerState.Idle:  SetActive(false); break;
            case PlayerState.Alive: SetActive(true); break;
            case PlayerState.Death:
                {
                    Debug.Log("死亡");
                    model.SetActive(false);                             // モデルを非表示
                    SetActive(false);
                    GameplayManager.Instance.GameOver();
                }
                break;
            case PlayerState.Reviving:
                {
                    Debug.Log("復活");
                    model.SetActive(false);                             // モデルを非表示
                    SetActive(true);
                }
                break;
        }
    }
}
