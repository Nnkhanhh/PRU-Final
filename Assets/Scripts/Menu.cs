using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
	public TextMeshProUGUI currentMapText; // Kéo vào inspector nếu muốn hiển thị map

	private void Start()
	{
		if (currentMapText != null)
			currentMapText.text = $"Bạn đang ở Map {GameSession.CurrentMap}";
	}
	public void ContinueGame()
	{
		int mapToLoad = GameSession.CurrentMap > 0 ? GameSession.CurrentMap : 1;
		SceneManager.LoadScene("Map" + mapToLoad);
	}
	public void PlayGame()
	{
		SceneManager.LoadScene("Map" + GameSession.CurrentMap);
	}
	public void QuitGame()
	{
		Application.Quit();
	}
}