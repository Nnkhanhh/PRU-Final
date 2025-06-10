using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager gameManager;
    private Health playerHealth;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        playerHealth = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(0.5);
            }
        }
        else if (collision.CompareTag("Slime"))
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(0.5);
            }
        }
    }
}