using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popups
{
    public class UnlockLevelPopup : BasePopup
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _unlockButton;
        [SerializeField] private TextMeshProUGUI _balanceText;
        [SerializeField] private TextMeshProUGUI _costText;

        private int _cost;
        private int _level;

        public event Action OnBackPressed;
        public event Action<int, int> OnUnlockPressed;

        public void Init(int level, int cost)
        {
            _level = level;
            _cost = cost;
            _costText.text = cost.ToString();
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());
            _unlockButton.onClick.AddListener(() => OnUnlockPressed?.Invoke(_level, _cost));
        }

        public void SetBalance(int balance)
        {
            _balanceText.text = balance.ToString();
        }
    }
}