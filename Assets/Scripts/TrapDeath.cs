using UnityEngine;

public class TrapDeath : MonoBehaviour
{
	public double currentHealth { get; private set; }
	[SerializeField] private float maxHealth = 5f; // Maximum health of the trap
	[SerializeField] private float initialHealth = 3f; // Initial health of the trap
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
			TakeDamage(10);
		}
	}
}
