using System;

namespace Game.UserData
{
    [Serializable]
    public class UserData
    {
        public SettingsData SettingsData = new SettingsData();
        public LoginData LoginData = new LoginData();
        public int Balance = 0;
        public int MaxScore = 0;
        public int Level = 1;
        public int CorrectAnswers = 0;
        public int CompletedAchievements = 0;
        public int Testings = 0;
    }
}