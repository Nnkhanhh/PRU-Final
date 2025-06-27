using UnityEngine;

public class TriggerQuizZone : MonoBehaviour
{
        private bool triggered = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (triggered) return;

            if (other.CompareTag("Player"))
            {
                triggered = true;

                other.GetComponent<PlayerController>().canMove = false;

                FindObjectOfType<QuizManager>().StartQuiz();
            }
        }
}
