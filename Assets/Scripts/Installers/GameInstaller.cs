using DefaultNamespace;
using Game.UserData;
using Runtime.Game.GameStates.Game.Screens;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameUIContainer _gameUIContainer;
        [SerializeField] private AudioManager _audioManager;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameBootstrapController>().AsSingle();
            Container.Bind<GameUIContainer>().FromComponentInNewPrefab(_gameUIContainer).AsSingle();
            Container.Bind<QuestionController>().AsSingle();

            BindStates();
            BindServices();

            Container.Bind<AudioButton>().FromComponentInHierarchy().AsTransient();
        }

        private void BindStates()
        {
            Container.Bind<StateController>().To<LoadingScreenStateController>().AsSingle();
            Container.Bind<StateController>().To<InfoScreenStateController>().AsSingle();
            Container.Bind<StateController>().To<ProfileScreenStateController>().AsSingle();
            Container.Bind<StateController>().To<MenuScreenStateController>().AsSingle();
            Container.Bind<StateController>().To<SettingsScreenStateController>().AsSingle();
            Container.Bind<StateController>().To<GameWheelScreenStateController>().AsSingle();
            Container.Bind<StateController>().To<LibraryScreenStateController>().AsSingle();
            Container.Bind<StateController>().To<LeaderboardScreenStateController>().AsSingle();
            Container.Bind<StateController>().To<AchievmentsScreenStateController>().AsSingle();
            Container.Bind<StateController>().To<PrivacyPolicyScreenStateController>().AsSingle();
            Container.Bind<StateController>().To<TermsScreenStateController>().AsSingle();
            Container.Bind<StateMachine>().AsSingle();
        }

        private void BindServices()
        {
            Container.Bind<SettingsProvider>().AsSingle();
            Container.Bind<AudioManager>().FromComponentInNewPrefab(_audioManager).AsSingle().NonLazy();
            Container.Bind<AudioService>().AsSingle().NonLazy();
        }
    }
}