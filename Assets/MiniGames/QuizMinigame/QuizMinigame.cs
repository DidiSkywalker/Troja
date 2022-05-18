using System.Linq;
using Events.Channels;
using Minigames;
using UnityEngine;
using UnityEngine.UIElements;

namespace MiniGames.QuizMinigame
{
    public class QuizMinigame : MonoBehaviour
    {
        public UIDocument ui;
        public TextAsset[] quizData;
        public MinigameSO minigameSO;
        public MinigameEventChannelSO minigameEventChannelSO;
        
        private Quiz _quiz;

        private Label _titleLabel;
        private Label _questionLabel;
        private VisualElement _answerContainer;
        private Button _nextButton;

        private int _currentQuestion;
        private bool _answered = false;

        private void Start()
        {
            _quiz = Quiz.ParseFromMultipleJsonSources(quizData.Select(textAsset => textAsset.text));
            InitUI();
            DisplayQuestion();
        }

        private void InitUI()
        {
            _titleLabel = ui.rootVisualElement.Q<Label>("title");
            _questionLabel = ui.rootVisualElement.Q<Label>("question");
            _answerContainer = ui.rootVisualElement.Q<VisualElement>("answers");
            _nextButton = ui.rootVisualElement.Q<Button>("next");
            _nextButton.clickable.clicked += OnClickNext;
        }

        private void DisplayQuestion()
        {
            // cleanup
            SetNextButtonVisible(false);
            _answerContainer.RemoveFromClassList("solution");
            _answerContainer.Clear();
            _answered = false;
            
            // display question and answers
            var question = _quiz.questions[_currentQuestion];
            _titleLabel.text = $"Question {_currentQuestion+1}/{_quiz.questions.Count}";
            _questionLabel.text = question.text;

            foreach (var label in question.answers.Select(MakeAnswerLabel))
            {
                _answerContainer.Add(label);
            }
        }

        private Label MakeAnswerLabel(Quiz.Answer answer)
        {
            var label = new Label(answer.text);
            label.AddToClassList("answer");
            if (answer.isCorrect)
            {
                label.AddToClassList("correct");
            }

            label.RegisterCallback<ClickEvent>(OnClickAnswer);
            return label;
        }

        private void OnClickAnswer(ClickEvent evt)
        {
            evt.PreventDefault();
            
            if (_answered) return;
            
            SetNextButtonVisible(true);
            _answerContainer.AddToClassList("solution");
            ((VisualElement)evt.target).AddToClassList("clicked");
            _answered = true;
        }

        private void OnClickNext()
        {
            if (_currentQuestion < _quiz.questions.Count - 1)
            {
                _currentQuestion++;
                DisplayQuestion();
            }
            else
            {
                minigameEventChannelSO.RaiseEvent(minigameSO);
            }
        }

        private void SetNextButtonVisible(bool show)
        {
            _nextButton.style.visibility = show ? Visibility.Visible : Visibility.Hidden;
        }
    }
}