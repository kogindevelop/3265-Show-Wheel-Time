using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class LibraryScreen : BaseScreen
    {
        [SerializeField] private TextMeshProUGUI _balanceText;
        [SerializeField] private Button _backButton;

        public BlocksManager BlocksManager;
        public LevelsManager LevelsManager;
        public CardsManager CardsManager;
        public HintDisplay HintDisplay;

        public LibraryLevel CurrentLibraryLevel;

        public event Action OnBackPressed;

        public void Initialize()
        {
            SubscribeEvents();
            ShowBlocks();
        }

        private void SubscribeEvents()
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());
        }

        public void ShowBlocks()
        {
            CurrentLibraryLevel = LibraryLevel.Blocks;
            BlocksManager.gameObject.SetActive(true);
            LevelsManager.gameObject.SetActive(false);
            CardsManager.gameObject.SetActive(false);
            HintDisplay.gameObject.SetActive(false);
        }

        public void ShowLevels()
        {
            CurrentLibraryLevel = LibraryLevel.Levels;
            BlocksManager.gameObject.SetActive(false);
            LevelsManager.gameObject.SetActive(true);
            CardsManager.gameObject.SetActive(false);
            HintDisplay.gameObject.SetActive(false);
        }

        public void ShowCards()
        {
            CurrentLibraryLevel = LibraryLevel.Cards;
            BlocksManager.gameObject.SetActive(false);
            LevelsManager.gameObject.SetActive(false);
            CardsManager.gameObject.SetActive(true);
            HintDisplay.gameObject.SetActive(false);
        }

        public void ShowHint()
        {
            CurrentLibraryLevel = LibraryLevel.Hints;
            BlocksManager.gameObject.SetActive(false);
            LevelsManager.gameObject.SetActive(false);
            CardsManager.gameObject.SetActive(false);
            HintDisplay.gameObject.SetActive(true);
        }

        public void SetBalance(int balance)
        {
            _balanceText.text = balance.ToString();
        }
    }
    public enum LibraryLevel
    {
        Blocks,
        Levels,
        Cards,
        Hints
    }
}