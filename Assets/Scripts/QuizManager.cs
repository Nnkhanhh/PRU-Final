using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Button[] optionButtons;

    private List<QuizQuestion> quizQuestions;
    private int currentQuestionIndex = 0;

    [SerializeField] private QuizDatabaseSQLServer quizDatabase;
    private QuizBoss currentBoss;

    void Start()
    {
        quizQuestions = quizDatabase.GetQuestions();
        if (quizQuestions.Count > 0)
            DisplayQuestion();
            
    }

    void DisplayQuestion()
    {
        QuizQuestion currentQuestion = quizQuestions[currentQuestionIndex];

        questionText.text = currentQuestion.QuestionText;
        optionButtons[0].GetComponentInChildren<Text>().text = currentQuestion.OptionA;
        optionButtons[1].GetComponentInChildren<Text>().text = currentQuestion.OptionB;
        optionButtons[2].GetComponentInChildren<Text>().text = currentQuestion.OptionC;
        optionButtons[3].GetComponentInChildren<Text>().text = currentQuestion.OptionD;

        // Assign listeners for answer buttons
        optionButtons[0].onClick.RemoveAllListeners();
        optionButtons[1].onClick.RemoveAllListeners();
        optionButtons[2].onClick.RemoveAllListeners();
        optionButtons[3].onClick.RemoveAllListeners();

        optionButtons[0].onClick.AddListener(() => OnAnswerSelected("A"));
        optionButtons[1].onClick.AddListener(() => OnAnswerSelected("B"));
        optionButtons[2].onClick.AddListener(() => OnAnswerSelected("C"));
        optionButtons[3].onClick.AddListener(() => OnAnswerSelected("D"));
    }

    public void ShowNextQuestion(QuizBoss boss)
    {
        if (currentQuestionIndex >= quizQuestions.Count)
        {
            UnityEngine.Debug.Log("No more questions!");
            return;
        }

        currentBoss = boss; // Remember which boss is asking

        QuizQuestion currentQuestion = quizQuestions[currentQuestionIndex];

        questionText.text = currentQuestion.QuestionText;
        optionButtons[0].GetComponentInChildren<Text>().text = currentQuestion.OptionA;
        optionButtons[1].GetComponentInChildren<Text>().text = currentQuestion.OptionB;
        optionButtons[2].GetComponentInChildren<Text>().text = currentQuestion.OptionC;
        optionButtons[3].GetComponentInChildren<Text>().text = currentQuestion.OptionD;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].interactable = true;
        }

        optionButtons[0].onClick.AddListener(() => OnAnswerSelected("A"));
        optionButtons[1].onClick.AddListener(() => OnAnswerSelected("B"));
        optionButtons[2].onClick.AddListener(() => OnAnswerSelected("C"));
        optionButtons[3].onClick.AddListener(() => OnAnswerSelected("D"));
    }

    void OnAnswerSelected(string selectedOption)
    {
        QuizQuestion currentQuestion = quizQuestions[currentQuestionIndex];

        if (selectedOption == currentQuestion.CorrectOption)
            UnityEngine.Debug.Log("Correct!");
        else
            UnityEngine.Debug.Log("Wrong!");

        currentQuestionIndex++;
        if (currentQuestionIndex < quizQuestions.Count)
            DisplayQuestion();
        else
            UnityEngine.Debug.Log("Quiz finished!");
    }
}
