using UnityEngine;


    [System.Serializable]
    public class QuizQuestion
    {
        public int QuizQuestionID;
        public int MapLevel;
        public string QuestionType;
        public string QuestionText;
        public string OptionA;
        public string OptionB;
        public string OptionC;
        public string OptionD;
        public string CorrectOption;
        public string OriginalVerb;
        public string VerbTense;
    }