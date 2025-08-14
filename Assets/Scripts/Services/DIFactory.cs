using UnityEngine;
using Zenject;

namespace Game.UserData
{
    public class DIFactory
    {
        private readonly DiContainer _container;

        public DIFactory(DiContainer container)
        {
            _container = container;
        }
        
        public T Create<T>(GameObject prefab) => _container.InstantiatePrefabForComponent<T>(prefab);
    }
}