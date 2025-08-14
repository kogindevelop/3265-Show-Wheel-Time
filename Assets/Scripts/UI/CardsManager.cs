using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _cardDisplayPrefab;

    public event Action<CardConfig> OnCardPressed;

    public void Init(BlockConfig blockConfig, Difficulty difficulty)
    {
        var child = _container.GetComponentsInChildren<CardDisplay>();
        foreach (var item in child)
        {
            Destroy(item.gameObject);
        }

        ClearCards();
        var cardSets = blockConfig.CardSets.Find(x => x.Difficulty == difficulty);
        foreach (var card in cardSets.Cards)
        {
            Create(card);
        }
    }

    private void Create(CardConfig cardConfig)
    {
        var id = cardConfig.ID;
        var name = cardConfig.Name;
        var blockDisplayPrefab = Instantiate(_cardDisplayPrefab, _container);
        var cardDisplay = blockDisplayPrefab.GetComponent<CardDisplay>();
        cardDisplay.Init(id, name);
        cardDisplay.Button.onClick.AddListener(() => OnCardPressed?.Invoke(cardConfig));
    }

    private void ClearCards()
    {
        var children = _container.GetComponentsInChildren<CardDisplay>();
        foreach (var item in children)
        {
            Destroy(item.gameObject);
        }
    }
}