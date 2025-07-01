using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTrigger : MonoBehaviour
{
    public string winSceneName = "WinUI"; // Tên Scene cần chuyển đến

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("▶ Player đã chạm vào cổng dịch chuyển!");
            SceneManager.LoadScene(winSceneName);
        }
    }
}
