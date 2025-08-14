using System.Threading;
using Cysharp.Threading.Tasks;
using UI.Screens;

namespace Runtime.Game.GameStates.Game.Screens
{
    public class PrivacyPolicyScreenStateController : StateController
    {
        private readonly GameUIContainer _uiContainer;

        private PrivacyPolicyScreen _screen;

        public PrivacyPolicyScreenStateController(GameUIContainer uiContainer)
        {
            _uiContainer = uiContainer;
        }

        public override async UniTask EnterState()
        {
            _screen = await _uiContainer.CreateScreen<PrivacyPolicyScreen>();
            _screen.Initialize();
            _screen.OnBackPressed += async () => await GoTo<MenuScreenStateController>();
        }

        public override async UniTask ExitState()
        {
            await _uiContainer.HideScreen<PrivacyPolicyScreen>();
        }
    }
}