using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class LeaderboardScreen : BaseScreen
    {
        [SerializeField] private Button _backButton;

        public LeaderboardManager LeaderboardManager;

        public event Action OnBackPressed;

        public void Initialize()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());
        }
    }
}