using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro; // <- THÊM DÒNG NÀY
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
    public GameObject winUI;
    public GameObject bossObject;
    public int mapLevel = 1;

    private List<QuizQuestion> questions;
    private int index = 0;
    private int correct = 0;
    private int wrong = 0;

    public void StartQuiz()
    {
        correct = 0;
        wrong = 0;
        index = 0;
        StartCoroutine(GetQuestions());
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

        if (selected.ToString() == correctOption)
            correct++;
        else
            wrong++;

        if (correct >= 20)
            Win();
        else if (wrong >= 5)
            Lose();
        else
        {
            index++;
            ShowNext();
        }
    }

    void Win()
    {
        winUI?.SetActive(true);
        bossObject?.SetActive(false);
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
