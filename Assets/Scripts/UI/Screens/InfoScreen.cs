using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class InfoScreen : BaseScreen
    {
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _startButton;
        [SerializeField] private List<GameObject> _panels;

        public int PanelsCount => _panels.Count;
        public int ActivePanel = 0;

        public event Action OnNextPressed;
        public event Action OnStartPressed;

        public void Initialize()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _nextButton.onClick.AddListener(SetNextPanelActive);
            _startButton.onClick.AddListener(() => OnStartPressed?.Invoke());
        }

        public void SetNextPanelActive()
        {
            ActivePanel++;

            if (ActivePanel >= _panels.Count - 1)
            {
                ActivePanel = _panels.Count - 1;
                ActiveStartButton();
            }

            foreach (var panel in _panels)
            {
                panel.SetActive(false);
            }

            _panels[ActivePanel].SetActive(true);
        }

        public void ActiveStartButton()
        {
            _startButton.gameObject.SetActive(true);
            _nextButton.gameObject.SetActive(true);
        }
    }
}