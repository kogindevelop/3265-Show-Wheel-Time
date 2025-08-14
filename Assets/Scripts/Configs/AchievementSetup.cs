using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementSetup", menuName = "Config/AchievementSetup")]
public class AchievementSetup : ScriptableObject
{
    public List<AchievementConfig> AchievementConfigs;
}