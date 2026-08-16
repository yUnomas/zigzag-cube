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
        if (state == newState) return;

        state = newState;
        switch(newState)
        {
            case PlayerState.Idle:  SetActive(false); break;
            case PlayerState.Alive: SetActive(true); break;
            case PlayerState.Dying:
                {
                    Debug.Log("死亡処理の開始");
                    SetActive(false);
                    model.SetActive(false); // モデルを非表示
                }
                break;
            case PlayerState.Death:
                {
                    Debug.Log("死亡処理の完了");
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
