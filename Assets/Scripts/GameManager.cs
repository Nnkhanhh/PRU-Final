using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUi;
    [SerializeField] private GameObject gameWinUi;
    private bool isGameOver = false;
    private bool isGameWin = false;
	private void Start()
    {
        gameOverUi.SetActive(false); 
        gameWinUi.SetActive(false);
	}

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0; 
        gameOverUi.SetActive(true); 
    }
    public void GameWin()
    {
        isGameWin = true;
        Time.timeScale = 0; 
        gameWinUi.SetActive(true);
	}
	public void RestarGame()
    {
        isGameOver = false;
        Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
    public void GotoMenu()
    {
        SceneManager.LoadScene("Menu"); // Replace with your main menu scene name
        Time.timeScale = 1;
    }

	public bool IsGameOver()
    {
        return isGameOver;
	}
    public bool IsGameWin()
    {
        return isGameWin;
    }
}
