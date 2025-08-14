using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Slider _progressSlider;
    [SerializeField] private Image _completedImage;

    public bool IsCompleted()
    {
      return  _progressSlider.value >= _progressSlider.maxValue;
    }

    public void Init(AchievementConfig achievementConfig, int progress)
    {
        _name.text = achievementConfig.Name;
        _description.text = achievementConfig.Description;
        _progressSlider.maxValue = achievementConfig.MaxProgress;
        _progressSlider.value = progress;
        _completedImage.enabled = progress >= achievementConfig.MaxProgress;
    }
}