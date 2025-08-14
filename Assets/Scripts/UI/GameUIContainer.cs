using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.UserData;
using UI.Popups;
using UI.Screens;
using UnityEngine;
using Zenject;

public class GameUIContainer : MonoBehaviour
{
    [SerializeField] private float _fadeTime = 0.25f;

    [SerializeField] private RectTransform _canvas;
    [SerializeField] private CanvasGroup _fadeCanvas;

    [SerializeField] private BaseScreen[] _screens;
    [SerializeField] private BasePopup[] _popups;

    private readonly Dictionary<Type, BaseScreen> _gameScreens = new();
    private readonly Dictionary<Type, BasePopup> _gamePopups = new();

    private readonly List<BaseScreen> _openedScreens = new();

    [Inject]
    private DIFactory _factory;

    private void Awake()
    {
        foreach (var screen in _screens)
            _gameScreens.Add(screen.GetType(), screen);

        foreach (var popup in _popups)
            _gamePopups.Add(popup.GetType(), popup);
    }

    public async UniTask<T> CreateScreen<T>() where T : BaseScreen
    {
        if (_gameScreens.TryGetValue(typeof(T), out BaseScreen prefab))
        {
            var screen = _factory.Create<T>(prefab.gameObject);
            _openedScreens.Add(screen);
            screen.transform.SetParent(_canvas, false);

            await FadeOut();

            return screen;
        }

        throw new Exception($"Screen {typeof(T)} not found");
    }

    public T GetScreen<T>() where T : BaseScreen
    {
        if (_gameScreens.TryGetValue(typeof(T), out BaseScreen prefab))
        {
            var screen = _factory.Create<T>(prefab.gameObject);
            _openedScreens.Add(screen);
            screen.transform.SetParent(_canvas, false);

            return screen;
        }

        throw new Exception($"Screen {typeof(T)} not found");
    }

    public async Task ShowScreenAsync()
    {
        await FadeOut();
    }

    public T CreatePopup<T>() where T : BasePopup
    {
        if (_gamePopups.TryGetValue(typeof(T), out BasePopup prefab))
        {
            var popup = _factory.Create<T>(prefab.gameObject);
            popup.transform.SetParent(_canvas, false);
            return popup;
        }

        throw new Exception($"Popup {typeof(T)} not found");
    }

    public async UniTask HideScreen<T>() where T : BaseScreen
    {
        for (var i = 0; i < _openedScreens.Count; i++)
            if (_openedScreens[i].GetType() == typeof(T))
            {
                var screen = _openedScreens[i];
                _openedScreens.RemoveAt(i);

                await FadeIn();

                Destroy(screen.gameObject);
            }
    }

    private async UniTask FadeIn()
    {
        _fadeCanvas.alpha = 0;
        _fadeCanvas.interactable = true;
        _fadeCanvas.blocksRaycasts = true;

        _fadeCanvas.DOFade(1, _fadeTime);
        await UniTask.WaitForSeconds(_fadeTime);

        _fadeCanvas.interactable = false;
        _fadeCanvas.blocksRaycasts = false;
    }

    private async UniTask FadeOut()
    {
        _fadeCanvas.alpha = 1;
        _fadeCanvas.interactable = true;
        _fadeCanvas.blocksRaycasts = true;

        _fadeCanvas.DOFade(0, _fadeTime);
        await UniTask.WaitForSeconds(_fadeTime);

        _fadeCanvas.interactable = false;
        _fadeCanvas.blocksRaycasts = false;
    }
}