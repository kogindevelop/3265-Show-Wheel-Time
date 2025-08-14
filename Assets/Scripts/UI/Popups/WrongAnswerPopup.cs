using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popups
{
    public class WrongAnswerPopup : BasePopup
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private HeartController _heartController;
        public Button Button;

        public void Init(int score, int hearts)
        {
            _scoreText.text = $"Score {score}";
            _heartController.InitializeHearts(hearts);
        }
    }
}