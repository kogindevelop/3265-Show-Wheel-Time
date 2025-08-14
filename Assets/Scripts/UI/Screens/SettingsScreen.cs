using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class SettingsScreen : BaseScreen
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Toggle _soundToggle;

        public event Action OnBackPressed;
        public event Action<float> OnMusicVolumeChange;
        public event Action<float> OnSoundVolumeChange;

        public void Initialize(float musicValue, float soundValue)
        {
            _musicToggle.isOn = musicValue > 0f;
            _soundToggle.isOn = soundValue > 0f;
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());
            _musicToggle.onValueChanged.AddListener(value => OnMusicVolumeChange?.Invoke(value ? 1 : 0));
            _soundToggle.onValueChanged.AddListener(value => OnSoundVolumeChange?.Invoke(value ? 1 : 0));
        }
    }
}