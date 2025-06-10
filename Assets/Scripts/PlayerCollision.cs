using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager gameManager;
    private Health playerHealth;

    private bool isInDangerZone = false;
    private float damageInterval = 1f;
    private float damageTimer = 0f;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        playerHealth = GetComponent<Health>();
    }

    private void Update()
    {
        if (isInDangerZone && playerHealth != null)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                playerHealth.TakeDamage(0.5);
                damageTimer = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap") || collision.CompareTag("Slime"))
        {
            isInDangerZone = true;
            damageTimer = damageInterval; // Apply damage immediately on enter
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap") || collision.CompareTag("Slime"))
        {
            isInDangerZone = false;
            damageTimer = 0f;
        }
    }
}
