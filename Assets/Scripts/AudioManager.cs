using Core.Services.Audio;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AudioManager : MonoBehaviour
{
    private SettingsProvider _staticSettingsService;

    [SerializeField] private List<AudioData> _audioDataList;

    [Range(0f, 1f)][SerializeField] private float _soundVolume = 1f;
    [Range(0f, 1f)][SerializeField] private float _musicVolume = 1f;

    private Dictionary<AudioClipID, AudioClip> _soundClips = new();
    private Dictionary<AudioClipID, AudioClip> _musicClips = new();

    private AudioSource _soundSource;
    private AudioSource _musicSource;

    private void Awake()
    {
        _soundSource = gameObject.AddComponent<AudioSource>();
        _soundSource.loop = false;
        _soundSource.playOnAwake = false;

        _musicSource = gameObject.AddComponent<AudioSource>();
        _musicSource.loop = true;
        _musicSource.playOnAwake = false;

        foreach (var data in _audioDataList)
        {
            if (data == null || data.Clip == null) continue;

            switch (data.AudioType)
            {
                case AudioType.Sound:
                    _soundClips[data.audioClipID] = data.Clip;
                    break;
                case AudioType.Music:
                    _musicClips[data.audioClipID] = data.Clip;
                    break;
            }
        }
    }
    /*
        public void PlaySound(AudioClipID id)
        {
            if (_soundClips.TryGetValue(id, out var clip))
            {
                _soundSource.PlayOneShot(clip, _soundVolume);
            }
            else
            {
                Debug.LogWarning($"[AudioManager] Sound with id '{id}' not found.");
            }
        }

        public void PlayMusic(AudioClipID id)
        {
            if (_musicClips.TryGetValue(id, out var clip))
            {
                _musicSource.clip = clip;
                _musicSource.volume = _musicVolume;
                _musicSource.Play();
            }
            else
            {
                Debug.LogWarning($"[AudioManager] Music with id '{id}' not found.");
            }
        }*/

    [Inject]
    public void Construct(SettingsProvider staticSettingsService)
    {
        _staticSettingsService = staticSettingsService;
    }


    public void PlayMusic(AudioClipID clipId)
    {
        var audioConfig = _staticSettingsService.Get<AudioConfig>();
        var clip = GetClip(audioConfig, clipId);
        if (clip)
            PlayMusic(clip, true);
    }

    public void PlaySound(AudioClipID clipId)
    {
        var audioConfig = _staticSettingsService.Get<AudioConfig>();
        var clip = GetClip(audioConfig, clipId);
        if (clip)
            PlaySound(clip);
    }

    private AudioClip GetClip(AudioConfig config, AudioClipID clipId)
    {
        var audioData = config.Audio.Find(x => x.audioClipID == clipId);
        return audioData?.Clip;
    }

    public void PlaySound(AudioClip clip, bool loop = false)
    {
        var source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.loop = loop;
        source.volume = _soundVolume;
        source.Play();

        if (!loop) Destroy(source, clip.length);
    }

    public void PlayMusic(AudioClip clip, bool loop = false)
    {
        _musicSource.clip = clip;
        _musicSource.loop = loop;
        _musicSource.volume = _musicVolume;
        _musicSource.Play();
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void StopAllSounds()
    {
        _soundSource.Stop();
    }

    public void StopAll()
    {
        _soundSource.Stop();
        _musicSource.Stop();
    }

    public void PauseAll()
    {
        _soundSource.Pause();
        _musicSource.Pause();
    }

    public void ResumeAll()
    {
        _soundSource.UnPause();
        _musicSource.UnPause();
    }

    public void ResumeSounds()
    {
        _soundSource.UnPause();
    }

    public bool IsPlaying(AudioClip clip)
    {
        return _musicSource.clip == clip && _musicSource.isPlaying;
    }

    public void SetSoundVolume(float volume)
    {
        _soundVolume = Mathf.Clamp01(volume);
    }

    public void SetMusicVolume(float volume)
    {
        _musicVolume = Mathf.Clamp01(volume);
        if (_musicSource.isPlaying)
            _musicSource.volume = _musicVolume;
    }

    public void SetVolume(AudioType type, float volume)
    {
        volume = Mathf.Clamp01(volume);
        switch (type)
        {
            case AudioType.Sound:
                _soundVolume = volume;
                break;
            case AudioType.Music:
                _musicVolume = volume;
                _musicSource.volume = volume;
                break;
        }
    }
}