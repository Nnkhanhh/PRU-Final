using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
	public void GoToNextMap()
	{
		int nextMapIndex = GameSession.CurrentMap + 1;

		var playfabLogin = Object.FindFirstObjectByType<PlayFabLogin>();
		if (playfabLogin != null)
		{
			playfabLogin.SaveCurrentMap(nextMapIndex, GameSession.TotalElapsedTime);
		}
		else
		{
			Debug.LogWarning("Không tìm thấy PlayFabLogin trong scene!");
		}
		GameSession.CurrentMap = nextMapIndex;
		SceneManager.LoadScene("Map" + nextMapIndex);
	}
}