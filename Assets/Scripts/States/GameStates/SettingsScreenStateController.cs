using System.Threading;
using Cysharp.Threading.Tasks;
using Game.UserData;
using UI.Screens;

namespace Runtime.Game.GameStates.Game.Screens
{
    public class SettingsScreenStateController : StateController
    {
        private readonly GameUIContainer _uiContainer;
        private readonly SaveSystem _saveSystem;
        private readonly AudioService _audioService;

        private SettingsScreen _screen;

        public SettingsScreenStateController(GameUIContainer uiContainer, SaveSystem saveSystem, AudioService audioService)
        {
            _uiContainer = uiContainer;
            _saveSystem = saveSystem;
            _audioService = audioService;
        }

        public override async UniTask EnterState()
        {
            _screen = _uiContainer.GetScreen<SettingsScreen>();

            var settingsData = _saveSystem.GetData().SettingsData;
            var musicValue = settingsData.MusicVolume;
            var soundValue = settingsData.SoundVolume;
            _screen.Initialize(musicValue, soundValue);

            await _uiContainer.ShowScreenAsync();

            _screen.OnBackPressed += async () =>
            {
                await GoTo<MenuScreenStateController>();
            };
            _screen.OnMusicVolumeChange += OnMusicVolumeChanged;
            _screen.OnSoundVolumeChange += OnSoundVolumeChanged;
        }

        public override async UniTask ExitState()
        {
            await _uiContainer.HideScreen<SettingsScreen>();
        }

        private void OnMusicVolumeChanged(float value)
        {
            _saveSystem.GetData().SettingsData.MusicVolume = value;
            _saveSystem.Save();
            _audioService.SetVolume(AudioType.Music, value);
        }

        private void OnSoundVolumeChanged(float value)
        {
            _saveSystem.GetData().SettingsData.SoundVolume = value;
            _saveSystem.Save();
            _audioService.SetVolume(AudioType.Sound, value);
        }
    }
}