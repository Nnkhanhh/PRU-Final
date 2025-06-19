using UnityEngine;

public class LavaDamage : MonoBehaviour
{
    [SerializeField] private float damageAmount = 2f;
    [SerializeField] private float damageInterval = 1f; // Deal damage every 1 second

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            StartCoroutine(ApplyBurn(collision.GetComponent<Health>()));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            StopAllCoroutines(); // Stops damage when player leaves
    }

    private System.Collections.IEnumerator ApplyBurn(Health playerHealth)
    {
        while (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
