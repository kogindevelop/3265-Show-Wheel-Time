using AssetsProvider;
using Game.UserData;
using Runtime.Game.Services.SettingsProvider;
using Zenject;

public class ProjectBootstrapController : IInitializable
{
    private readonly PrefabsProvider _prefabsProvider;
    private readonly ConfigsProvider _configsProvider;
    private readonly SaveSystem _saveSystem;

    public ProjectBootstrapController(
        PrefabsProvider prefabsProvider,
        ConfigsProvider configsProvider,
        SaveSystem saveSystem)
    {
        _prefabsProvider = prefabsProvider;
        _configsProvider = configsProvider;
        _saveSystem = saveSystem;
    }

    public async void Initialize()
    {
        _saveSystem.Load();
        await _prefabsProvider.Initialize();
        await _configsProvider.Initialize();
    }
}
