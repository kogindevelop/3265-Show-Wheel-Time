using Game.UserData;
using Runtime.Game.GameStates.Game.Screens;
using Zenject;

namespace DefaultNamespace
{
    public class GameBootstrapController : IInitializable
    {
        private readonly StateMachine _stateMachine;
        private readonly DiContainer _container;
        private readonly SettingsProvider _settingsProvider;
        private readonly AudioService _audioService;

        public GameBootstrapController(StateMachine stateMachine, DiContainer container, SettingsProvider settingsProvider, AudioService audioService)
        {
            _stateMachine = stateMachine;
            _container = container;
            _settingsProvider = settingsProvider;
            _audioService = audioService;
        }

        public async void Initialize()
        {
            _container.InstantiateComponentOnNewGameObject<ApplicationStateListener>("ApplicationStateListener");
            await _settingsProvider.Initialize();
            _audioService.Init();
            await _stateMachine.ChangeState<LoadingScreenStateController>();
        }
    }
}