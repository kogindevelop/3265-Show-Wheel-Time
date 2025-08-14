using System.Threading;
using Cysharp.Threading.Tasks;
using Game.UserData;
using UI.Screens;

namespace Runtime.Game.GameStates.Game.Screens
{
    public class MenuScreenStateController : StateController
    {
        private readonly GameUIContainer _uiContainer;
        private readonly SaveSystem _saveSystem;

        private MenuScreen _screen;

        public MenuScreenStateController(GameUIContainer uiContainer, SaveSystem saveSystem)
        {
            _uiContainer = uiContainer;
            _saveSystem = saveSystem;
        }

        public override async UniTask EnterState()
        {
            _screen = _uiContainer.GetScreen<MenuScreen>();
            _screen.Initialize();
            _screen.OnProfilePressed += async () => { await GoTo<ProfileScreenStateController>(); };
            _screen.OnSettingsPressed += async () => { await GoTo<SettingsScreenStateController>(); };
            _screen.OnWheelPressed += async () => { await GoTo<GameWheelScreenStateController>(); };
            _screen.OnLibraryPressed += async () => { await GoTo<LibraryScreenStateController>(); };
            _screen.OnLeadersPressed += async () => { await GoTo<LeaderboardScreenStateController>(); };
            _screen.OnAchievementsPressed += async () => { await GoTo<AchievmentsScreenStateController>(); };
            UpdateUI();
            await _uiContainer.ShowScreenAsync();
        }

        public override async UniTask ExitState()
        {
            await _uiContainer.HideScreen<MenuScreen>();
        }

        private void UpdateUI()
        {
            _screen.SetBalance(_saveSystem.GetData().Balance);
        }
    }
}