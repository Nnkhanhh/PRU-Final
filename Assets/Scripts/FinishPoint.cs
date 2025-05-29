using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Assuming you have a GameManager or similar to handle the game state
            //GameManager.Instance.CompleteLevel();
        }
    }
}
