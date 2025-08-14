using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI.Popups
{
    public class QuizPopup : BasePopup
    {
        [SerializeField] private TextMeshProUGUI _questionName;
        [SerializeField] private TextMeshProUGUI _balanceText;
        [SerializeField] private HeartController _heartController;
        [SerializeField] private List<AnswerDisplay> _answerDisplays;
        [SerializeField] private Color _correctColor;
        [SerializeField] private Color _wrongColor;
        [SerializeField] private Color _normalColor;
        [SerializeField] private TextMeshProUGUI _timeText;

        private bool _answered;
        private const int MaxTime = 15;

        public event Action<bool> OnAnswer;

        public void Init(Question question, int hearts, int balance)
        {
            _balanceText.text = balance.ToString();
            _questionName.text = question.Name;
            _heartController.InitializeHearts(hearts);
            _answered = false;

            for (int i = 0; i < _answerDisplays.Count; i++)
            {
                _answerDisplays[i].Init(question.Answers[i]);
            }

            foreach (var answerDisplay in _answerDisplays)
            {
                answerDisplay.Button.onClick.AddListener(() => CheckAnswers(answerDisplay));
                answerDisplay.Button.interactable = true;
                answerDisplay.SetColor(_normalColor);
            }

            StartTimerAsync().Forget();
        }

        private async UniTaskVoid StartTimerAsync()
        {
            int timeLeft = MaxTime;

            while (timeLeft >= 0 && !_answered)
            {
                _timeText.text = $"00:{timeLeft:00}";
                await UniTask.Delay(TimeSpan.FromSeconds(1));
                timeLeft--;
            }

            if (!_answered)
            {
                SkipTurn();
            }
        }

        private void SkipTurn()
        {
            foreach (var answer in _answerDisplays)
            {
                if (answer.CheckIsCorrect())
                {
                    answer.SetColor(_correctColor);
                }
                answer.Button.interactable = false;
            }

            _answered = true;
            InvokeWithDelayAsync(false);
        }

        public void CheckAnswers(AnswerDisplay answerDisplay)
        {
            if (_answered) return;
            _answered = true;

            var isCorrect = answerDisplay.CheckIsCorrect();

            if (!isCorrect)
                answerDisplay.SetColor(_wrongColor);

            foreach (var answer in _answerDisplays)
            {
                if (answer.CheckIsCorrect())
                {
                    answer.SetColor(_correctColor);
                }
                answer.Button.interactable = false;
            }
            InvokeWithDelayAsync(isCorrect);
        }

        private async void InvokeWithDelayAsync(bool isCorrect)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2f));
            OnAnswer?.Invoke(isCorrect);
        }
    }
}