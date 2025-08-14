using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksManager : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _blockDisplayPrefab;
    [SerializeField] private BlockSetup _setup;

    public event Action<BlockConfig> OnBlockPressed;

    public void Init()
    {
        var child = _container.GetComponentsInChildren<BlockDisplay>();
        foreach (var item in child)
        {
            Destroy(item.gameObject);
        }

        foreach (var block in _setup.Blocks)
        {
            Create(block);
        }
    }

    private void Create(BlockConfig blockConfig)
    {
        var id = blockConfig.ID;
        var name = blockConfig.Name;

        var blockDisplayPrefab = Instantiate(_blockDisplayPrefab, _container);
        var blockDisplay = blockDisplayPrefab.GetComponent<BlockDisplay>();
        blockDisplay.Init(id, name);
        blockDisplay.Button.onClick.AddListener(() => OnBlockPressed?.Invoke(blockConfig));
    }
}