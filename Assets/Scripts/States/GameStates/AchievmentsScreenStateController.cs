using System.Threading;
using Cysharp.Threading.Tasks;
using Game.UserData;
using UI.Screens;

namespace Runtime.Game.GameStates.Game.Screens
{
    public class AchievmentsScreenStateController : StateController
    {
        private readonly GameUIContainer _uiContainer;
        private readonly SaveSystem _saveSystem;

        private AchievmentsScreen _screen;

        public AchievmentsScreenStateController(GameUIContainer uiContainer, SaveSystem saveSystem)
        {
            _uiContainer = uiContainer;
            _saveSystem = saveSystem;
        }

        public override async UniTask EnterState()
        {
            _screen = _uiContainer.GetScreen<AchievmentsScreen>();
            _screen.AchievementsManager.Init(_saveSystem);
            await _uiContainer.ShowScreenAsync();
            _screen.Initialize();
            _screen.OnBackPressed += async () => await GoTo<MenuScreenStateController>();
        }

        public override async UniTask ExitState()
        {
            await _uiContainer.HideScreen<AchievmentsScreen>();
        }
    }
}