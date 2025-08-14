using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popups
{
    public class NotEnoughBalancePopup : BasePopup
    {
        [SerializeField] private Button _backButton;

        public event Action OnBackPressed;

        public void Init()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());
        }
    }
}