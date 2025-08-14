using System;
using System.IO;
using UnityEngine;

namespace Game.UserData
{
    public class SaveSystem
    {
        private UserData _userData;

        private static string FilePath => Path.Combine(Application.persistentDataPath, "userdata.json");

        public UserData GetData()
        {
            return _userData;
        }

        public void Save()
        {
            try
            {
                var json = JsonUtility.ToJson(_userData, true);
                File.WriteAllText(FilePath, json);
                Debug.Log($"[SaveSystem] Saved to {FilePath}");
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveSystem] Failed to save data: {e}");
            }
        }

        public void Load()
        {
            if (!File.Exists(FilePath))
            {
                Debug.Log("[SaveSystem] Save file not found. Creating new UserData.");
                _userData = new UserData();
                return;
            }

            try
            {
                var json = File.ReadAllText(FilePath);
                Debug.Log($"[SaveSystem] Loaded JSON: {json}");

                _userData = JsonUtility.FromJson<UserData>(json) ?? new UserData();
                Debug.Log("[SaveSystem] Load successful.");
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[SaveSystem] Failed to load save file. Using new UserData. Error: {e}");
                _userData = new UserData();
            }
        }
    }
}