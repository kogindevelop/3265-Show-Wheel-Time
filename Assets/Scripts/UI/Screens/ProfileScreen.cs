using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class ProfileScreen : BaseScreen
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _termsButton;
        [SerializeField] private Button _privacyButton;
        [SerializeField] private TextMeshProUGUI _ReachedQuestions;
        [SerializeField] private TextMeshProUGUI _BestScore;
        [SerializeField] private TextMeshProUGUI _completedAchievement;
        public AchievementSetup AchievementSetup;

        public event Action OnBackPressed;
        public event Action OnTermsPressed;
        public event Action OnPrivacyPressed;

        public void Initialize(int questions, int score, int achievement)
        {
            _ReachedQuestions.text = $"Reached {questions} questions";
            _BestScore.text = $"Best score : {score}";
            _completedAchievement.text = $"Achievements {achievement}/9";
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());
            _termsButton.onClick.AddListener(() => OnTermsPressed?.Invoke());
            _privacyButton.onClick.AddListener(() => OnPrivacyPressed?.Invoke());
        }
    }
}