using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
  private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            gameManager.GameOver();
            Debug.Log("Game Over UI bật lên");
        }
        else if (collision.CompareTag("Slime"))
        {
            gameManager.GameOver();
            Debug.Log("Cham vao SLime");
        }
    }
}
