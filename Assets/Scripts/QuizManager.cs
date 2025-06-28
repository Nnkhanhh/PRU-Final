using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class QuizQuestion
{
    public int questionId;
    public string questionType;
    public string questionText;
    public string optionA;
    public string optionB;
    public string optionC;
    public string optionD;
    public string correctOption;
}


public class QuizManager : MonoBehaviour
{
    public TMP_Text questionText; // <- PHẢI LÀ TMP_Text nếu bạn dùng TextMeshPro
    public Button[] optionButtons;
    public GameObject panelQuiz;
    public GameObject gameOverUI;
    public GameObject bossObject;
    public int mapLevel = 1;
    public bool isQuizActive = false; // Biến này để kiểm tra trạng thái quiz

    public Animator bossAnimator;
    public Animator playerAnimator;
    private BossBehaviour boss;


    private List<QuizQuestion> questions;
    private int index = 0;
    private int correct = 0;
    private int wrong = 0;

    public void StartQuiz()
    {
        boss = FindObjectOfType<BossBehaviour>();
        panelQuiz.SetActive(false);
        correct = 0;
        wrong = 0;
        index = 0;
        StartCoroutine(GetQuestions());
    }

    public bool IsQuizActive()
    {
        return isQuizActive = true; // Trả về trạng thái quiz
    }

    IEnumerator GetQuestions()
    {
        string url = $"http://localhost:5217/api/Quiz/map/{mapLevel}/random";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("❌ API Error: " + www.error);
        }
        else
        {
            Debug.Log("🛰 API RAW: " + www.downloadHandler.text);

            var json = "{\"questions\":" + www.downloadHandler.text + "}";
            QuizListWrapper wrapper = JsonUtility.FromJson<QuizListWrapper>(json);

            if (wrapper == null || wrapper.questions == null || wrapper.questions.Length == 0)
            {
                Debug.LogWarning("⚠️ Không parse được câu hỏi.");
                yield break;
            }

            questions = new List<QuizQuestion>(wrapper.questions);
            ShowNext();
        }
    }

    void ShowNext()
    {
        if (index >= questions.Count)
        {
            Debug.Log("✔️ Đã hoàn tất danh sách câu hỏi.");
            if (correct >= 7) // hoặc >= số lượng bạn muốn để win
            {
                Win();
            }
            else
            {
                Lose(); // hoặc có thể gọi lại quiz, hoặc hiện thông báo
            }

            return;
        }

        QuizQuestion q = questions[index];
        questionText.text = q.questionText;

        string[] options = { q.optionA, q.optionB, q.optionC, q.optionD };

        for (int i = 0; i < optionButtons.Length; i++)
        {
            int idx = i;
            TMP_Text btnText = optionButtons[i].GetComponentInChildren<TMP_Text>();
            if (btnText != null)
                btnText.text = options[i];
            else
                Debug.LogError("❌ Button thiếu TMP_Text");

            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() =>
            {
                HandleAnswer((char)('A' + idx), q.correctOption);
            });
        }

        panelQuiz.SetActive(true);
    }

    void HandleAnswer(char selected, string correctOption)
    {
        panelQuiz.SetActive(false);

        if (string.IsNullOrEmpty(selected.ToString()))
        {
            return; 
        }

        if (selected.ToString() == correctOption)
        {
            correct++;

            if (boss != null)
            {
                float damagePerCorrect = boss.HitPointslMax / 10f;
                boss.TakeHit(damagePerCorrect);
            }
            if (bossAnimator != null)
            {
                bossAnimator.SetTrigger("BossHurt");
            }
            if (playerAnimator != null)
            {
                int randomAttack = Random.Range(1, 4);

                switch (randomAttack)
                {
                    case 1:
                        playerAnimator.SetTrigger("Attack1");
                        break;
                    case 2:
                        playerAnimator.SetTrigger("Attack2");
                        break;
                    case 3:
                        playerAnimator.SetTrigger("Attack3");
                        break;
                }
            }
        }
        else
        {
            wrong++;

            var playerHealth = FindObjectOfType<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(0.5);
            }
            if (playerAnimator != null)
            {
                playerAnimator.SetTrigger("hurt");
            }
            if (bossAnimator != null)
            {
                bossAnimator.Play("BossMap2", 0, 0f);

                if (Random.Range(1, 3) == 1)
                {
                    bossAnimator.SetTrigger("BossAttack1");
                }
                else
                {
                    bossAnimator.SetTrigger("BossAttack2");
                }
            }
        }

        var playerHealth1 = FindObjectOfType<Health>();
        if (playerHealth1 != null && playerHealth1.currentHealth <= 0)
        {
            StartCoroutine(HandlePlayerDeath());
            return;
        }

        if (boss != null && boss.HitPoints <= 0)
        {
            Win(); 
            return; 
        }
        else if (wrong >= 10)
            Lose();
        else
        {
            index++;
            ShowNext();
        }
    }
    IEnumerator HandlePlayerDeath()
    {
        yield return new WaitForSeconds(1.5f);

        gameOverUI?.SetActive(true);
        Time.timeScale = 0f;
    }

    IEnumerator PlayBossDeathThenWin()
    {
        if (bossAnimator != null)
        {
            bossAnimator.SetTrigger("BossHurt");

            float hurtTime = GetAnimationLength("BossHurt");
            yield return new WaitForSeconds(hurtTime);

            bossAnimator.SetTrigger("BossDeath");

            float deathTime = GetAnimationLength("BossDead");
            yield return new WaitForSeconds(deathTime);
        }

        bossObject?.SetActive(false);
        FindObjectOfType<PlayerController>().canMove = true;
        panelQuiz.SetActive(false);
    }
    float GetAnimationLength(string clipName)
    {
        foreach (var clip in bossAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
                return clip.length;
        }
        return 1f; 
    }

    void Win()
    {
        StartCoroutine(PlayBossDeathThenWin());
    }

    void Lose()
    {
        gameOverUI?.SetActive(true);
        Time.timeScale = 0f;
    }

    [System.Serializable]
    public class QuizListWrapper
    {
        public QuizQuestion[] questions;
    }
}
