using System.Collections.Generic;
using Core;
using Core.Services.Audio;
using Game.UserData;
using UnityEngine;

public class AudioService
{
    private readonly SaveSystem _saveSystem;
    private readonly SettingsProvider _staticSettingsService;
    private readonly AudioManager _audioManager;

    public AudioService(SettingsProvider staticSettingsService, AudioManager audioManager, SaveSystem saveSystem)
    {
        _staticSettingsService = staticSettingsService;
        _audioManager = audioManager;
        _saveSystem = saveSystem;
    }

    public void Init()
    {
        var data = _saveSystem.GetData().SettingsData;
        var musicVolume = data.MusicVolume;
        var soundVolume = data.SoundVolume;

        SetVolume(AudioType.Music, musicVolume);
        SetVolume(AudioType.Sound, soundVolume);
        PlayMusic(AudioClipID.Music_bg);
    }

    public void PlayMusic(AudioClipID clipId)
    {
        var audioConfig = _staticSettingsService.Get<AudioConfig>();
        var clip = GetClip(audioConfig, clipId);
        if (clip)
            _audioManager.PlayMusic(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        _audioManager.PlayMusic(clip);
    }

    public void PlaySound(AudioClipID clipId)
    {
        var audioConfig = _staticSettingsService.Get<AudioConfig>();
        var clip = GetClip(audioConfig, clipId);
        if (clip)
            _audioManager.PlaySound(clip);
    }

    public void PlaySound(AudioClipID clipId, bool loop)
    {
        var audioConfig = _staticSettingsService.Get<AudioConfig>();
        var clip = GetClip(audioConfig, clipId);
        if (clip)
            _audioManager.PlaySound(clip, loop);
    }

    public bool IsPlaying(AudioClipID clipId)
    {
        var audioConfig = _staticSettingsService.Get<AudioConfig>();
        var clip = GetClip(audioConfig, clipId);
        return clip != null && _audioManager.IsPlaying(clip);
    }

    public void PauseAll()
    {
        _audioManager.PauseAll();
    }

    public void ResumeAll()
    {
        _audioManager.ResumeAll();
    }

    public void ResumeSounds()
    {
        _audioManager.ResumeSounds();
    }

    public void StopMusic()
    {
        _audioManager.StopMusic();
    }

    public void StopAllSounds()
    {
        _audioManager.StopAllSounds();
    }

    public void StopAll()
    {
        _audioManager.StopAll();
    }

    public void SetVolume(AudioType audioType, float volume)
    {
        _audioManager.SetVolume(audioType, volume);
    }

    private AudioClip GetClip(AudioConfig config, AudioClipID clipId)
    {
        var audioData = config.Audio.Find(x => x.audioClipID == clipId);
        return audioData?.Clip;
    }
}