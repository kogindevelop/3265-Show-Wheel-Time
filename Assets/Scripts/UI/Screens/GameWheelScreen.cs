using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class GameWheelScreen : BaseScreen
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _startButton;
        [SerializeField] private TextMeshProUGUI _balanceText;
        [SerializeField] private TextMeshProUGUI _questionText;

        public WheelSpinner WheelSpinner;
        public HeartController HeartController;
        public QuestionController QuestionController;

        public event Action OnBackPressed;
        public event Action OnStartPressed;

        public void Initialize()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());
            _startButton.onClick.AddListener(() => OnStartPressed?.Invoke());
        }

        public void SetButtonActive(bool active)
        {
            _backButton.interactable = active;
            _startButton.interactable = active;
        }

        public void SetBalance(int balance)
        {
            _balanceText.text = balance.ToString();
        }

        public void SetQuestion(int used, int all)
        {
            _questionText.text = $"Question {used}/{all}";
        }
    }
}