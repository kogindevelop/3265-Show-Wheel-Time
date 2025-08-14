using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popups
{
    public class LosePopup : BasePopup
    {
        [SerializeField] private HeartController _heartController;
        [SerializeField] private Button _homeButton;

        public event Action OnHomeButtonPressed;

        public void Init(int hearts)
        {
            _heartController.InitializeHearts(hearts);
            _homeButton.onClick.AddListener(() => OnHomeButtonPressed?.Invoke());
        }
    }
}