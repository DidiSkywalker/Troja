using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MiniGames.QuizMinigame
{
    [Serializable]
    public class Quiz
    {
        [Serializable]
        public class Question
        {
            public string text;
            public Answer[] answers;

            public bool IsValid()
            {
                return !(string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text)) &&
                       answers.Any(ans => ans.isCorrect) && answers.All(ans => ans.IsValid());
            }
        }

        [Serializable]
        public class Answer
        {
            public string text;
            public bool isCorrect;

            public bool IsValid()
            {
                return !(string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text));
            }

            public override string ToString()
            {
                var prefix = isCorrect ? "[c]" : "[x]";
                return $"{prefix} {text}";
            }
        }

        public List<Question> questions = new();

        public static Quiz ParseFromJson(string json)
        {
            var quiz = JsonUtility.FromJson<Quiz>(json);
            if (!quiz.questions.All(question => question.IsValid()))
            {
                Debug.LogWarning(
                    "One or more questions or answers are invalid." +
                    " Make sure texts are not empty or whitespace and every" +
                    " question has at least one correct answer");
            }

            return quiz;
        }

        public static Quiz ParseFromMultipleJsonSources(IEnumerable<string> jsonTexts)
        {
            return jsonTexts.ToList()
                .Select(ParseFromJson)
                .Aggregate(new Quiz(), (acc, source) =>
                {
                    acc.questions.AddRange(source.questions);
                    return acc;
                });
        }
    }
}