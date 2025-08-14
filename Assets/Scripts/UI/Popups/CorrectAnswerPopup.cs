using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popups
{
    public class CorrectAnswerPopup : BasePopup
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        public Button Button;

        public void Init(int score)
        {
            _scoreText.text = $"Score {score}";
        }
    }
}