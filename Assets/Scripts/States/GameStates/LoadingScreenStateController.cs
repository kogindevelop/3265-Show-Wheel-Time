using System.Threading;
using Cysharp.Threading.Tasks;
using Game.UserData;
using UI.Screens;

namespace Runtime.Game.GameStates.Game.Screens
{
    public class LoadingScreenStateController : StateController
    {
        private readonly GameUIContainer _uiContainer;
        private readonly SaveSystem _saveSystem;

        private LoadingScreen _screen;

        public LoadingScreenStateController(GameUIContainer uiContainer, SaveSystem saveSystem)
        {
            _uiContainer = uiContainer;
            _saveSystem = saveSystem;
        }

        public override async UniTask EnterState()
        {
            _screen = await _uiContainer.CreateScreen<LoadingScreen>();
            await _screen.SplashScreenAnimation();

            var isFirstEnter = _saveSystem.GetData().LoginData.isFirstEnter;
            if (isFirstEnter)
                await GoTo<InfoScreenStateController>();
            else
                await GoTo<MenuScreenStateController>();
        }

        public override async UniTask ExitState()
        {
            await _uiContainer.HideScreen<LoadingScreen>();
        }

    }
}