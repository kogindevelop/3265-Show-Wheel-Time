using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _levelDisplayPrefab;
    [SerializeField] private Image _lockImage;
    [SerializeField] private Image _panelImage;

    private int _level;

    public event Action<Difficulty> OnLevelPressed;
    public event Action<int, int> OnBuyPressed;

    public void Init(int level)
    {
        _level = level;
        var allDifficulties = Enum.GetValues(typeof(Difficulty)).Cast<Difficulty>().ToList();
        var id = 1;

        var child = _container.GetComponentsInChildren<LevelDisplay>();
        foreach (var item in child)
        {
            Destroy(item.gameObject);
        }

        foreach (var block in allDifficulties)
        {
            var locked = false;
            if ((int)block >= _level)
            {
                locked = true;
            }

            var cost = (int)block * 200;
            Create(id, block, locked, cost);
            id++;
        }
    }

    private void Create(int id, Difficulty difficulty, bool locked, int cost)
    {
        var name = difficulty.ToString();

        var blockDisplayPrefab = Instantiate(_levelDisplayPrefab, _container);
        var levelDisplay = blockDisplayPrefab.GetComponent<LevelDisplay>();
        levelDisplay.Init(id, name, locked);
        levelDisplay.SetCost(cost);
        levelDisplay.LevelButton.onClick.AddListener(() => OnLevelPressed?.Invoke(difficulty));
        levelDisplay.LockButton.onClick.AddListener(() => OnBuyPressed?.Invoke(_level, levelDisplay.GetCost()));

        if (id >= _level + 2)
        {
            levelDisplay.LockButton.interactable = false;
        }
    }
}