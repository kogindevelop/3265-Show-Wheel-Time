using System.Threading;
using Cysharp.Threading.Tasks;
using Game.UserData;
using UI.Screens;
using UserProfile;

namespace Runtime.Game.GameStates.Game.Screens
{
    public class LeaderboardScreenStateController : StateController
    {
        private readonly GameUIContainer _uiContainer;
        private readonly SaveSystem _saveSystem;

        private LeaderboardScreen _screen;

        public LeaderboardScreenStateController(GameUIContainer uiContainer,SaveSystem saveSystem)
        {
            _uiContainer = uiContainer;
            _saveSystem = saveSystem;
        }

        public override async UniTask EnterState()
        {
            _screen = _uiContainer.GetScreen<LeaderboardScreen>();
            _screen.Initialize();
            var name = UserProfileStorage.UserName;
            var score = _saveSystem.GetData().MaxScore;
            _screen.LeaderboardManager.Init(name,score);
            _screen.OnBackPressed += async () => await GoTo<MenuScreenStateController>();
            await _uiContainer.ShowScreenAsync();
        }

        public override async UniTask ExitState()
        {
            await _uiContainer.HideScreen<LeaderboardScreen>();
        }
    }
}