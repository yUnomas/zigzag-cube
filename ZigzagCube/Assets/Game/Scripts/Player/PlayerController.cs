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
                    model.SetActive(false); // モデルを非表示
                    GameplayManager.Instance.GameOver();
                    ChangeState(PlayerState.Idle);
                }
                break;
            case PlayerState.Revive:
                {
                    Debug.Log("復活");
                    model.SetActive(true);  // モデルを表示
                    GetComponent<PlayerRevive>().Revive();
                }
                break;
        }
    }
}
