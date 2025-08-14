using System.Threading;
using Cysharp.Threading.Tasks;
using Game.UserData;
using UI.Popups;
using UI.Screens;
using UnityEngine;

namespace Runtime.Game.GameStates.Game.Screens
{
    public class GameWheelScreenStateController : StateController
    {
        private readonly GameUIContainer _uiContainer;
        private readonly SaveSystem _saveSystem;
        private readonly AudioService _audioService;

        private GameWheelScreen _screen;
        private int _score = 0;
        private int _hearts = 6;

        public GameWheelScreenStateController(GameUIContainer uiContainer, SaveSystem saveSystem, AudioService audioService)
        {
            _uiContainer = uiContainer;
            _saveSystem = saveSystem;
            _audioService = audioService;
        }

        public override async UniTask EnterState()
        {
            _score = 0;
            _hearts = 6;

            _screen = _uiContainer.GetScreen<GameWheelScreen>();
            _screen.Initialize();

            _screen.QuestionController.InitializeQuestions(_saveSystem.GetData().Level);

            _screen.SetBalance(_saveSystem.GetData().Balance);

            _screen.WheelSpinner.OnWheelPrize += CheckPrize;
            //_screen.HeartController.OnHeartBroken += () => { Debug.Log("Heart Break"); };
            UpdateUI();
            await _uiContainer.ShowScreenAsync();

            _screen.OnBackPressed += async () =>
            {
                await GoTo<MenuScreenStateController>();
            };
            _screen.OnStartPressed += () =>
            {
                Spin();
            };

            ContinueGame();
        }

        private void UpdateUI()
        {
            var used = _screen.QuestionController.GetUsedQuestionCount();
            var all = _screen.QuestionController.GetAllQuestionCount();
            _screen.SetQuestion(used, all);

            _screen.HeartController.InitializeHearts(_hearts);
        }

        private void Spin()
        {
            _screen.WheelSpinner.StartSpin();
            _screen.SetButtonActive(false);
        }

        public override async UniTask ExitState()
        {
            await _uiContainer.HideScreen<GameWheelScreen>();
        }

        private void CheckPrize(WheelPrize wheelPrize)
        {
            if (wheelPrize == WheelPrize.Restart)
            {
                ContinueGame();
            }
            else if (wheelPrize == WheelPrize.Heart)
            {
                _hearts = Mathf.Min(_hearts + 1, 6);
                ContinueGame();
            }
            else
            {
                ShowQuestionPopup(wheelPrize);
            }
        }

        private void ShowQuestionPopup(WheelPrize wheelPrize)
        {
            _audioService.PlaySound(AudioClipID.Popup_open);

            var question = new Question();
            if (wheelPrize == WheelPrize.Random)
            {
                _screen.QuestionController.TryGetRandomQuestion(out question);
            }
            else
            {
                _screen.QuestionController.TryGetNextQuestion(wheelPrize, out question);
            }

            var popup = _uiContainer.CreatePopup<QuizPopup>();

            var balance = _saveSystem.GetData().Balance;
            popup.Init(question, _hearts, balance);
            popup.OnAnswer += (value) =>
            {
                ShowResult(value);
                popup.HidePopup();
            };
        }

        private void ShowResult(bool isCorrect)
        {
            if (isCorrect)
            {
                _score += 100;
                _saveSystem.GetData().CorrectAnswers += 1;
                _saveSystem.Save();
                var popup = _uiContainer.CreatePopup<CorrectAnswerPopup>();
                popup.Init(_score);
                popup.Button.onClick.AddListener(() =>
                {
                    popup.HidePopup();
                    CheckIsGameOver();
                    _audioService.PlaySound(AudioClipID.Button_click);
                });
            }
            else
            {
                _hearts -= 1;
                _screen.HeartController.InitializeHearts(_hearts);
                var popup = _uiContainer.CreatePopup<WrongAnswerPopup>();
                popup.Init(_score, _hearts);
                popup.Button.onClick.AddListener(() =>
                {
                    popup.HidePopup();
                    CheckIsGameOver();
                    _audioService.PlaySound(AudioClipID.Button_click);
                });
            }
        }

        private void CheckIsGameOver()
        {
            if (IsGameOver())
            {
                var isWin = true;

                if (_hearts <= 0)
                {
                    isWin = false;
                }

                ShowGameOverPopup(isWin);
            }
            else
            {
                ContinueGame();
            }
        }

        private void ContinueGame()
        {
            UpdateUI();
            _screen.SetButtonActive(true);
        }

        private bool IsGameOver()
        {
            var isGameOver = false;

            if (_hearts <= 0 || !_screen.QuestionController.HasAnyQuestionsLeft())
            {
                isGameOver = true;
            }

            return isGameOver;
        }

        private void ShowGameOverPopup(bool isWin)
        {
            if (isWin)
            {
                _audioService.PlaySound(AudioClipID.Win_sound);
                var amount = _score / 10;

                _saveSystem.GetData().Balance += amount;
                _saveSystem.GetData().MaxScore = Mathf.Max(_score, _saveSystem.GetData().MaxScore);
                _saveSystem.GetData().Testings += 1;
                _saveSystem.Save();
                var popup = _uiContainer.CreatePopup<WinPopup>();
                popup.Init(_score, amount, _hearts);
                popup.OnHomeButtonPressed += () =>
                {
                    popup.HidePopup();
                    ToHome();
                };
            }
            else
            {
                _audioService.PlaySound(AudioClipID.Lose_sound);
                var popup = _uiContainer.CreatePopup<LosePopup>();
                popup.Init(0);
                popup.OnHomeButtonPressed += () =>
                {
                    popup.HidePopup();
                    ToHome();
                };
            }
        }

        private async void ToHome()
        {
            await GoTo<MenuScreenStateController>();
        }
    }
}