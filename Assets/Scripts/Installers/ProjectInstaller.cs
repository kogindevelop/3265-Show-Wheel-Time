using AssetsProvider;
using Game.UserData;
using Runtime.Core.Infrastructure.AssetProvider;
using Runtime.Game.Services.SettingsProvider;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ProjectBootstrapController>().AsSingle();
            Container.Bind<DIFactory>().AsSingle();
            BindAddressables();
            BindSaveSystem();
        }

        private void BindAddressables()
        {
            Container.Bind<AssetProvider>().AsSingle();
            Container.Bind<PrefabsProvider>().AsSingle();
            Container.Bind<ConfigsProvider>().AsSingle();
        }

        private void BindSaveSystem()
        {
            Container.Bind<SaveSystem>().AsSingle();
        }
    }
}