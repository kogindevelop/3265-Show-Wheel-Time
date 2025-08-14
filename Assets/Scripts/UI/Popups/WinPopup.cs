using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popups
{
    public class WinPopup : BasePopup
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _balanceText;
        [SerializeField] private Button _homeButton;
        [SerializeField] private HeartController _heartController;

        public event Action OnHomeButtonPressed;

        public void Init(int score, int amount, int hearts)
        {
            _scoreText.text = $"Score {score}";
            _balanceText.text = $"+{amount}";
            _heartController.InitializeHearts(hearts);
            _homeButton.onClick.AddListener(() => OnHomeButtonPressed?.Invoke());
        }
    }
}