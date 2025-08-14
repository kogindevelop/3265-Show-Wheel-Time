using System.Threading;
using Cysharp.Threading.Tasks;
using Game.UserData;
using UI.Popups;
using UI.Screens;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;

namespace Runtime.Game.GameStates.Game.Screens
{
    public class LibraryScreenStateController : StateController
    {
        private readonly GameUIContainer _uiContainer;
        private readonly SaveSystem _saveSystem;

        private LibraryScreen _screen;
        private BlockConfig _blockConfig;
        private Difficulty _difficulty;

        public LibraryScreenStateController(GameUIContainer uiContainer, SaveSystem saveSystem)
        {
            _uiContainer = uiContainer;
            _saveSystem = saveSystem;
        }

        public override async UniTask EnterState()
        {
            _screen = _uiContainer.GetScreen<LibraryScreen>();
            _screen.Initialize();
            UpdateUI();
            _screen.OnBackPressed += OnBackButtonPressed;
            UpdateConfigs();
            await _uiContainer.ShowScreenAsync();
        }

        private void UpdateConfigs()
        {
            _screen.BlocksManager.Init();
            _screen.BlocksManager.OnBlockPressed += (blockConfig) => OnBlocksButtonPressed(blockConfig);
            _screen.LevelsManager.Init(_saveSystem.GetData().Level);
            _screen.LevelsManager.OnLevelPressed += (difficulty) => OnLevelButtonPressed(difficulty);
            _screen.LevelsManager.OnBuyPressed += (level, cost) => OnLockButtonPressed(level, cost);
            _screen.CardsManager.OnCardPressed += (cardConfig) => OnCardButtonPressed(cardConfig);
        }

        private void UpdateUI()
        {
            _screen.SetBalance(_saveSystem.GetData().Balance);
        }

        private void OnBlocksButtonPressed(BlockConfig blockConfig)
        {
            _blockConfig = blockConfig;
            _screen.ShowLevels();
        }

        private void OnLevelButtonPressed(Difficulty difficulty)
        {
            _difficulty = difficulty;
            _screen.CardsManager.Init(_blockConfig, _difficulty);
            _screen.ShowCards();
        }

        private void OnLockButtonPressed(int level, int cost)
        {
            ShowUnlockPopup(level, cost);
        }

        private void ShowUnlockPopup(int level, int cost)
        {
            var popup = _uiContainer.CreatePopup<UnlockLevelPopup>();
            popup.Init(level, cost);
            popup.SetBalance(_saveSystem.GetData().Balance);
            popup.OnBackPressed += () => popup.HidePopup();

            popup.OnUnlockPressed += (level, cost) =>
            {
                var balance = _saveSystem.GetData().Balance;
                if (balance >= cost)
                {
                    _saveSystem.GetData().Level = level + 1;
                    _saveSystem.GetData().Balance -= cost;
                    _saveSystem.Save();
                    UpdateUI();
                    UpdateConfigs();
                }
                else
                {
                    ShowNotEnoughBalancePopup();
                }
                popup.HidePopup();
            };
        }

        private void ShowNotEnoughBalancePopup()
        {
            UpdateUI();
            var popup = _uiContainer.CreatePopup<NotEnoughBalancePopup>();
            popup.Init();
            popup.OnBackPressed += () => popup.HidePopup();
        }

        private void OnCardButtonPressed(CardConfig cardConfig)
        {
            _screen.HintDisplay.Init(cardConfig);
            _screen.ShowHint();
        }

        private void OnBackButtonPressed()
        {
            if (_screen.CurrentLibraryLevel == LibraryLevel.Blocks)
            {
                _ = GoTo<MenuScreenStateController>();
            }

            if (_screen.CurrentLibraryLevel == LibraryLevel.Levels)
            {
                _screen.ShowBlocks();
            }

            if (_screen.CurrentLibraryLevel == LibraryLevel.Cards)
            {
                _screen.ShowLevels();
            }

            if (_screen.CurrentLibraryLevel == LibraryLevel.Hints)
            {
                _screen.ShowCards();
            }
        }

        public override async UniTask ExitState()
        {
            await _uiContainer.HideScreen<LibraryScreen>();
        }
    }
}