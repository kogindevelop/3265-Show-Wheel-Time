using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class LoadingScreen : BaseScreen
    {
        [SerializeField] private float _loadTime;
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _loadText;

        public async UniTask SplashScreenAnimation()
        {
            _slider.value = 0;
            _slider.maxValue = 100;
            float elapsed = 0f;

            while (elapsed < _loadTime)
            {
                elapsed += Time.deltaTime;
                float progress = Mathf.Clamp01(elapsed / _loadTime);
                int percent = Mathf.RoundToInt(progress * 100f);
                SetPercent(percent);

                await UniTask.Yield();
            }
        }

        private void SetPercent(int percent)
        {
            _loadText.text = $"{percent}%";
            _slider.value = percent;
        }
    }
}