using UnityEngine;

public class FinishPoint : MonoBehaviour
{
	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			// Tăng map
			GameSession.CurrentMap++;
			Debug.Log("[FinishPoint] CurrentMap sau khi tăng: " + GameSession.CurrentMap);

			// Lưu map mới lên PlayFab
			PlayFabLogin playFabLogin = FindObjectOfType<PlayFabLogin>();
			if (playFabLogin != null)
			{
				Debug.Log("[FinishPoint] Gọi SaveCurrentMap");
				playFabLogin.SaveCurrentMap(GameSession.CurrentMap);
			}
			else
			{
				Debug.LogError("[FinishPoint] Không tìm thấy PlayFabLogin");
			}

			// Chuyển màn
			SceneController.instance.NextLevel();
			Debug.Log("[FinishPoint] Chuyển màn thành công");
		}
	}
}