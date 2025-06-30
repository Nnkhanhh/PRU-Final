using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUI : MonoBehaviour
{
    public GameObject entryPrefab;
    public Transform entryContainer;
    public GameObject leaderboardPanel;
    public TextMeshProUGUI title;
    void Start()
    {
        leaderboardPanel.SetActive(false);
        title.color = Color.white;

    }

    public void ShowLeaderboard()
    {
        leaderboardPanel.SetActive(true);
        ClearEntries();

        var request = new GetLeaderboardRequest
        {
            StatisticName = "TotalMapTime",
            StartPosition = 0,
            MaxResultsCount = 10
        };

        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardSuccess, OnLeaderboardError);
    }

    void OnLeaderboardSuccess(GetLeaderboardResult result)
    {
        foreach (var entry in result.Leaderboard)
        {
            GameObject item = Instantiate(entryPrefab, entryContainer);

            TextMeshProUGUI nameText = item.transform.Find("NameText")?.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI timeText = item.transform.Find("TimeText")?.GetComponent<TextMeshProUGUI>();

            if (nameText != null && timeText != null)
            {
                float timeInSeconds = Mathf.Abs(entry.StatValue) / 1000f;
                nameText.text = $"{entry.Position + 1}. {entry.DisplayName ?? entry.PlayFabId}";
                timeText.text = $"{timeInSeconds:F2}s";
            }
        }
    }

    void OnLeaderboardError(PlayFabError error)
    {
        UnityEngine.Debug.LogError("[Leaderboard] Failed to fetch leaderboard: " + error.GenerateErrorReport());
    }

    void ClearEntries()
    {
        foreach (Transform child in entryContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void HideLeaderboard()
    {
        leaderboardPanel.SetActive(false);
    }
}
