using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    private bool isGameOver = false;
    private void Start()
    {
        gameOverUI.SetActive(false); 
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0; 
        gameOverUI.SetActive(true); 
    }
}
