using Game.UserData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _recordPrefab;
    [SerializeField] private AchievementSetup _achievementSetup;

    private SaveSystem _saveSystem;

    public void Init(SaveSystem saveSystem)
    {
        _saveSystem = saveSystem;

        foreach (var achievementConfig in _achievementSetup.AchievementConfigs)
        {
            Create(achievementConfig);
        }

        var achievementDisplays = _container.GetComponentsInChildren<AchievementDisplay>();
        var completed = 0;
        foreach (var display in achievementDisplays)
        {
            if (display.IsCompleted())
                completed++;
        }
        _saveSystem.GetData().CompletedAchievements = completed;
        _saveSystem.Save();
    }

    private void Create(AchievementConfig achievementConfig)
    {
        var progress = GetProgress(achievementConfig.Type);
        var achievementDisplayPrefab = Instantiate(_recordPrefab, _container);
        var AchievementDisplay = achievementDisplayPrefab.GetComponent<AchievementDisplay>();
        AchievementDisplay.Init(achievementConfig, progress);
    }

    private int GetProgress(AchievementType achievementType)
    {
        var progress = 0;

        if (achievementType == AchievementType.Testings)
        {
            progress = _saveSystem.GetData().Testings;
        }
        else if (achievementType == AchievementType.CorrectAnswers)
        {
            progress = _saveSystem.GetData().CorrectAnswers;
        }
        else if (achievementType == AchievementType.UnlockedLevel)
        {
            progress = _saveSystem.GetData().Level - 1;
        }

        return progress;
    }
}