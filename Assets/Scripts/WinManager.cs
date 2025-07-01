using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public void GotoMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

}
