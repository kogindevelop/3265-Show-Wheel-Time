using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _descriptionText;

    public void Init(CardConfig cardConfig)
    {
        string title = cardConfig.Name;
        string description = cardConfig.Description;
        _titleText.text = title;
        _descriptionText.text = description;
    }
}