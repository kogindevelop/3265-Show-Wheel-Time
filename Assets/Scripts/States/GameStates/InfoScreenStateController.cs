using System.Threading;
using Cysharp.Threading.Tasks;
using Game.UserData;
using UI.Screens;
using UnityEngine.Rendering;

namespace Runtime.Game.GameStates.Game.Screens
{
    public class InfoScreenStateController : StateController
    {
        private readonly GameUIContainer _uiContainer;
        private readonly SaveSystem _saveSystem;

        private InfoScreen _screen;

        public InfoScreenStateController(GameUIContainer uiContainer, SaveSystem saveSystem)
        {
            _uiContainer = uiContainer;
            _saveSystem = saveSystem;
        }

        public override async UniTask EnterState()
        {
            _screen = await _uiContainer.CreateScreen<InfoScreen>();
            _screen.Initialize();
            SubscribeToEvent();
        }

        private void SubscribeToEvent()
        {
            _screen.OnStartPressed += () => OnStartPressed();
        }

        private void UnsubscribeToEvent()
        {
            _screen.OnStartPressed -= () => OnStartPressed();
        }

        public override async UniTask ExitState()
        {
            _saveSystem.GetData().LoginData.isFirstEnter = false;
            _saveSystem.Save();
            await _uiContainer.HideScreen<InfoScreen>();
        }

        private async void OnStartPressed()
        {
            UnsubscribeToEvent();
            await GoTo<MenuScreenStateController>();
        }
    }
}