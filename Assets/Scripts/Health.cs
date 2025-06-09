using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 5;
    [SerializeField] private float initialHealth = 3;

    public double currentHealth { get; private set; }

    private Animator animator;
    private bool isDead;

    private void Awake()
    {
        currentHealth = initialHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(double damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0 && !isDead)
        {
            currentHealth = 0;
            animator.SetTrigger("die");
            GetComponent<PlayerController>().enabled = false;
            isDead = true;
        }
        else if (!isDead)
        {
            animator.SetTrigger("hurt");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(0.5);
        }
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp((float)(currentHealth + value), 0, maxHealth);
    }
}