using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementConfig", menuName = "Config/AchievementConfig")]
public class AchievementConfig : ScriptableObject
{
    public AchievementType Type;
    public string Name;
    public string Description;
    public int MaxProgress;
}

public enum AchievementType
{
    Testings,
    CorrectAnswers,
    UnlockedLevel
}