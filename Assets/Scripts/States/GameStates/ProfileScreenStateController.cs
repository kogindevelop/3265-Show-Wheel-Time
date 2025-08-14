using System.Threading;
using Cysharp.Threading.Tasks;
using Game.UserData;
using UI.Screens;
using UnityEngine;

namespace Runtime.Game.GameStates.Game.Screens
{
    public class ProfileScreenStateController : StateController
    {
        private readonly GameUIContainer _uiContainer;
        private readonly SaveSystem _saveSystem;

        private ProfileScreen _screen;

        public ProfileScreenStateController(GameUIContainer uiContainer, SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            _uiContainer = uiContainer;
        }

        public override async UniTask EnterState()
        {
            _screen = _uiContainer.GetScreen<ProfileScreen>();
            Init();
            var data = _saveSystem.GetData();
            int questions = data.CorrectAnswers;
            int score = data.MaxScore;
            int achievement = data.CompletedAchievements;
            _screen.Initialize(questions, score, achievement);
            _screen.OnBackPressed += async () =>
            {
                await GoTo<MenuScreenStateController>();
            };
            _screen.OnPrivacyPressed += async () => await GoTo<PrivacyPolicyScreenStateController>();
            _screen.OnTermsPressed += async () => await GoTo<TermsScreenStateController>();
            await _uiContainer.ShowScreenAsync();
        }

        public override async UniTask ExitState()
        {
            await _uiContainer.HideScreen<ProfileScreen>();
        }

        public void Init()
        {
            var completed = 0;
            foreach (var achievementConfig in _screen.AchievementSetup.AchievementConfigs)
            {
                if (IsCompleted(achievementConfig))
                    completed++;
            }

            _saveSystem.GetData().CompletedAchievements = completed;
            _saveSystem.Save();
        }

        private bool IsCompleted(AchievementConfig achievementConfig)
        {
            var progress = GetProgress(achievementConfig.Type);
            return progress >= achievementConfig.MaxProgress;
        }

        private int GetProgress(AchievementType achievementType)
        {
            var progress = 0;

            if (achievementType == AchievementType.Testings)
            {
                progress = _saveSystem.GetData().Testings;
            }
            else if (achievementType == AchievementType.CorrectAnswers)
            {
                progress = _saveSystem.GetData().CorrectAnswers;
            }
            else if (achievementType == AchievementType.UnlockedLevel)
            {
                progress = _saveSystem.GetData().Level - 1;
            }

            return progress;
        }
    }
}