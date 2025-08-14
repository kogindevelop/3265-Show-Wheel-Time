using System.Threading;
using Cysharp.Threading.Tasks;
using UI.Screens;

namespace Runtime.Game.GameStates.Game.Screens
{
    public class TermsScreenStateController : StateController
    {
        private readonly GameUIContainer _uiContainer;
        
        private TermsScreen _screen;
        
        public TermsScreenStateController(GameUIContainer uiContainer)
        {
            _uiContainer = uiContainer;
        }
        
        public override async UniTask EnterState()
        {
            _screen = await _uiContainer.CreateScreen<TermsScreen>();
            _screen.Initialize();
            _screen.OnBackPressed += async () => await GoTo<MenuScreenStateController>();
        }
        
        public override async UniTask ExitState()
        {
            await _uiContainer.HideScreen<TermsScreen>();
        }   
    }
}