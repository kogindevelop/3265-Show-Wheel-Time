using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class MenuScreen : BaseScreen
    {
        [SerializeField] private TextMeshProUGUI _balanceText;
        [SerializeField] private Button _profileButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _libraryButton;
        [SerializeField] private Button _wheelButton;
        [SerializeField] private Button _leadersButton;
        [SerializeField] private Button _achievementsButton;

        public event Action OnProfilePressed;
        public event Action OnSettingsPressed;
        public event Action OnLibraryPressed;
        public event Action OnWheelPressed;
        public event Action OnLeadersPressed;
        public event Action OnAchievementsPressed;

        public void Initialize()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _profileButton.onClick.AddListener(() => OnProfilePressed?.Invoke());
            _settingsButton.onClick.AddListener(() => OnSettingsPressed?.Invoke());
            _libraryButton.onClick.AddListener(() => OnLibraryPressed?.Invoke());
            _wheelButton.onClick.AddListener(() => OnWheelPressed?.Invoke());
            _leadersButton.onClick.AddListener(() => OnLeadersPressed?.Invoke());
            _achievementsButton.onClick.AddListener(() => OnAchievementsPressed?.Invoke());
        }

        public void SetBalance(int balance)
        {
            _balanceText.text = balance.ToString();
        }
    }
}