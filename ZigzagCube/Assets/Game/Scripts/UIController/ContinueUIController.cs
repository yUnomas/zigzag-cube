using UnityEngine;

public class ContinueUIController : UIControllerBase
{
    public void Continue()
    {
        GameplayManager.Instance.OnSelectedContinue();
        Hide();
    }
    public void GiveUp()
    {
        GameplayManager.Instance.OnSelectedGiveUp();
        Hide();
    }
}
