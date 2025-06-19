using UnityEngine;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.CodeDom;

public class QuizDatabaseSQLServer : MonoBehaviour
{
    private string connectionString = "Server=(local);Database=QuizGameDB;User Id=sa;Password=12345;";

    private List<QuizQuestion> questions = new List<QuizQuestion>();

    public void Start()
    {
        LoadQuestionsFromDatabase(1); // Example: Load questions for MapLevel = 1
    }

    void LoadQuestionsFromDatabase(int mapLevel)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = @"
                    SELECT QuizQuestionID, MapLevel, QuestionType, QuestionText, 
                           OptionA, OptionB, OptionC, OptionD, CorrectOption, 
                           OriginalVerb, VerbTense
                    FROM QuizQuestions
                    WHERE MapLevel = @mapLevel";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@mapLevel", mapLevel);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            QuizQuestion q = new QuizQuestion
                            {
                                QuizQuestionID = reader.GetInt32(0),
                                MapLevel = reader.GetInt32(1),
                                QuestionType = reader.GetString(2),
                                QuestionText = reader.GetString(3),
                                OptionA = reader.GetString(4),
                                OptionB = reader.GetString(5),
                                OptionC = reader.GetString(6),
                                OptionD = reader.GetString(7),
                                CorrectOption = reader.GetString(8),
                                OriginalVerb = reader.IsDBNull(9) ? null : reader.GetString(9),
                                VerbTense = reader.IsDBNull(10) ? null : reader.GetString(10)
                            };

                            questions.Add(q);
                        }
                    }
                }
                UnityEngine.Debug.Log("Questions loaded successfully from database.");
            }
            catch (SqlException ex)
            {
                UnityEngine.Debug.LogError("SQL Error: " + ex.Message);
            }
        }
    }
    public List<QuizQuestion> GetQuestions()
    {
        return questions;
    }
}

