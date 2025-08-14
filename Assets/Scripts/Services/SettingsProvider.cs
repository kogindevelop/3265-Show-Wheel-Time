using System;
using System.Collections.Generic;
using Core.Services.Audio;
using Cysharp.Threading.Tasks;
using Runtime.Core.Infrastructure.AssetProvider;
using UnityEngine;

public class SettingsProvider
{
    private readonly AssetProvider _assetProvider;

    private Dictionary<Type, ScriptableObject> _settings = new Dictionary<Type, ScriptableObject>();

    public SettingsProvider(AssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }

    public async UniTask Initialize()
    {
        var audioConfig = await _assetProvider.Load<AudioConfig>(nameof(AudioConfig));
        Set(audioConfig);
    }

    public T Get<T>() where T : ScriptableObject
    {
        if (_settings.ContainsKey(typeof(T)))
        {
            var setting = _settings[typeof(T)];
            return setting as T;
        }
        Debug.Log(typeof(T));
        throw new Exception("No setting found");
    }

    public void Set(ScriptableObject config)
    {
        if (_settings.ContainsKey(config.GetType()))
            return;

        _settings.Add(config.GetType(), config);
    }
}