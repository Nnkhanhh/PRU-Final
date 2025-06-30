using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public int finalMapIndex = 6; // You can also expose this in the Inspector

    public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			// Tăng map
			GameSession.CurrentMap++;
			Debug.Log("[FinishPoint] CurrentMap sau khi tăng: " + GameSession.CurrentMap);
            
			// 1. Stop level timer
            float levelTime = LevelTimer.Instance != null ? LevelTimer.Instance.StopTimer() : 0f;
            Debug.Log($"[FinishPoint] Time for this map: {levelTime:F2} seconds");

            // 2. Add this map's time to total
            GameSession.TotalElapsedTime += levelTime;
            Debug.Log($"[FinishPoint] Total elapsed time: {GameSession.TotalElapsedTime:F2} seconds");

            // Lưu map mới lên PlayFab
            PlayFabLogin playFabLogin = FindObjectOfType<PlayFabLogin>();
			if (playFabLogin != null)
			{
				Debug.Log("[FinishPoint] Gọi SaveCurrentMap");
				playFabLogin.SaveCurrentMap(GameSession.CurrentMap, GameSession.TotalElapsedTime);
                Debug.Log("[FinishPoint] Saved map + time to PlayFab");

            }
            else
			{
				Debug.LogError("[FinishPoint] Không tìm thấy PlayFabLogin");
			}

            // Check if it's the final map
            if (GameSession.CurrentMap == finalMapIndex)
            {
                Debug.Log($"Submit time: {GameSession.TotalElapsedTime:F2} seconds");
                SubmitFinalTimeToLeaderboard(GameSession.TotalElapsedTime);
            }

            // Chuyển màn
            SceneController.instance.NextLevel();
			Debug.Log($"[FinishPoint] Chuyển màn thành công");
		}
	}
    private void SubmitFinalTimeToLeaderboard(float totalTime)
    {
        int timeMs = Mathf.RoundToInt(totalTime * 1000f); // Convert to milliseconds

        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "TotalMapTime",
                    Value = -timeMs // Negative so lower time ranks higher
                }
            }
        };
        Debug.Log("[Leaderboard] Submitting total time now...");
        PlayFabClientAPI.UpdatePlayerStatistics(request,
            result => Debug.Log($"[Leaderboard] Final time submitted: {totalTime:F2}s"),
            error => Debug.LogError("[Leaderboard] Failed to submit score: " + error.GenerateErrorReport()));
    }
}