using UnityEngine;
using VoxelBusters.EssentialKit;

public class SocialShareModule : MonoBehaviour
{
    /// <summary>
    /// ゲーム結果を共有    </summary>
    public void ShareResult(ResultData resultData)
    {
        ShareSheet shareSheet = ShareSheet.CreateInstance();
        shareSheet.AddText($"{resultData.score} on #ZigzagCube.");
        shareSheet.AddScreenshot();
        shareSheet.SetCompletionCallback((result, error) => {
            Debug.Log("Share Sheet was closed. Result code: " + result.ResultCode);
        });
        shareSheet.Show();
    }
}
