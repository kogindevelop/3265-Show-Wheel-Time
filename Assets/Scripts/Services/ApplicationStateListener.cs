using System;
using UnityEngine;
using Zenject;

namespace Game.UserData
{
    public class ApplicationStateListener : MonoBehaviour
    {
        [Inject]
        private SaveSystem _saveSystem;
        
        private void OnApplicationQuit() => _saveSystem.Save();

        private void OnApplicationPause(bool pauseStatus)
        {
            if(pauseStatus)
                _saveSystem.Save();
        }
    }
}