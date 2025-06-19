using System.Diagnostics;
using UnityEngine;

public class QuizBoss : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    private int currentHealth;

    [SerializeField] private QuizManager quizManager;

    void Start()
    {
        currentHealth = maxHealth;
        AskQuestion();
    }

    public void TakeDamage()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            AskQuestion();
        }
    }

    private void AskQuestion()
    {
        quizManager.ShowNextQuestion(this);
    }

    private void Die()
    {
        UnityEngine.Debug.Log("Boss defeated!");        // Play animation, drop rewards, etc.
        Destroy(gameObject);
    }

    public void PlayerAnswered(bool isCorrect)
    {
        if (isCorrect)
        {
            TakeDamage();
        }
        else
        {
            AttackPlayer();
            AskQuestion(); // Ask again after attack
        }
    }

    private void AttackPlayer()
    {
        UnityEngine.Debug.Log("Boss attacks the player!");
        // Call player.TakeDamage() here
    }
}
