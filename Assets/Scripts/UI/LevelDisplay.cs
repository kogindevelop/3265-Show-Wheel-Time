using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _numberText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _lockImage;
    [SerializeField] private Image _lockPanel;

    private int _cost;

    public Button LevelButton;
    public Button LockButton;

    public void Init(int num, string name, bool locked)
    {
        _numberText.text = $"level {num}";
        _nameText.text = name;
        LockButton.gameObject.SetActive(locked);
        _lockImage.gameObject.SetActive(locked);
        _lockPanel.gameObject.SetActive(locked);
    }

    public void SetCost(int cost)
    {
        _cost = cost;
    }

    public int GetCost()
    {
        return _cost;
    }
}
